using System;
using System.Collections.Generic;
using AirportSurvival.Core;
using UnityEngine;

namespace AirportSurvival.Gameplay
{
    public sealed class AirportGameController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private int startingStamina = 100;

        [Header("Manager")]
        [SerializeField] private int scoldingStaminaPenalty = 15;

        [Header("Cleaning")]
        [SerializeField] private int pointsPerCleanedTrash = 10;
        [SerializeField] private int moneyPerCleanedTrash = 2;

        [Header("Day")]
        [SerializeField] private float dayLengthSeconds = 120f;

        private readonly HashSet<JobCategory> practicedJobs = new HashSet<JobCategory>();
        private PlayerProgress playerProgress;
        private ReprimandSystem reprimands;
        private DayResolutionService dayResolution;
        private float timeRemaining;
        private int cleanedTrash;
        private bool dayEnded;

        public event Action StateChanged;
        public event Action<string> ManagerDialogue;
        public event Action<AirportDaySummary> DayEnded;

        public PlayerProgress PlayerProgress => playerProgress;
        public float TimeRemaining => timeRemaining;
        public int CleanedTrash => cleanedTrash;
        public bool IsDayEnded => dayEnded;

        private void Awake()
        {
            playerProgress = new PlayerProgress(startingStamina);
            reprimands = new ReprimandSystem(scoldingStaminaPenalty);
            dayResolution = new DayResolutionService();
            timeRemaining = dayLengthSeconds;
            practicedJobs.Add(JobCategory.Cleaning);
        }

        private void Update()
        {
            if (dayEnded)
            {
                return;
            }

            timeRemaining = Mathf.Max(0f, timeRemaining - Time.deltaTime);
            if (timeRemaining <= 0f)
            {
                EndDay();
                return;
            }

            StateChanged?.Invoke();
        }

        public void RegisterTrashCleaned()
        {
            if (dayEnded)
            {
                return;
            }

            cleanedTrash++;
            practicedJobs.Add(JobCategory.Cleaning);
            playerProgress.AddJobPoints(JobCategory.Cleaning, pointsPerCleanedTrash);
            playerProgress.AddMoney(moneyPerCleanedTrash);
            StateChanged?.Invoke();
        }

        public void TriggerManagerScolding(string reason)
        {
            if (dayEnded)
            {
                return;
            }

            ReprimandResult result = reprimands.Scold(playerProgress, reason);
            ManagerDialogue?.Invoke(result.DialogueLine);
            StateChanged?.Invoke();
        }

        public void EndDay()
        {
            if (dayEnded)
            {
                return;
            }

            dayEnded = true;
            DayResolutionResult result = dayResolution.ResolveDay(playerProgress, practicedJobs);
            AirportDaySummary summary = new AirportDaySummary(
                cleanedTrash,
                cleanedTrash * pointsPerCleanedTrash,
                cleanedTrash * moneyPerCleanedTrash,
                playerProgress.Stamina,
                playerProgress.ScoldCount,
                result.DecayByJob);

            StateChanged?.Invoke();
            DayEnded?.Invoke(summary);
        }
    }
}
