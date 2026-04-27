using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportSurvival.Core
{
    [Serializable]
    public sealed class PlayerProgress
    {
        private readonly Dictionary<JobCategory, JobProgress> jobs;

        public PlayerProgress(int startingStamina = 100, int maxLevel = 5, int pointsPerLevel = 100)
        {
            Stamina = startingStamina;
            Money = 0;
            ScoldCount = 0;
            jobs = Enum.GetValues(typeof(JobCategory))
                .Cast<JobCategory>()
                .ToDictionary(
                    category => category,
                    category => new JobProgress(category, maxLevel, pointsPerLevel));
        }

        public int Stamina { get; private set; }
        public int Money { get; private set; }
        public int ScoldCount { get; private set; }
        public IReadOnlyDictionary<JobCategory, JobProgress> Jobs => jobs;
        public bool CanTakeOverAirport => jobs.Values.All(job => job.IsMaxLevel);

        public JobProgress GetJob(JobCategory category)
        {
            return jobs[category];
        }

        public void AddMoney(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            Money += amount;
        }

        public void AddJobPoints(JobCategory category, int amount)
        {
            jobs[category].AddPoints(amount);
        }

        public void ApplyScolding(int staminaPenalty)
        {
            if (staminaPenalty < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(staminaPenalty));
            }

            ScoldCount++;
            Stamina = Math.Max(0, Stamina - staminaPenalty);
        }
    }
}
