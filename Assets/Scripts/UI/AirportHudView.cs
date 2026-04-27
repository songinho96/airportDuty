using AirportSurvival.Core;
using AirportSurvival.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace AirportSurvival.UI
{
    public sealed class AirportHudView : MonoBehaviour
    {
        [SerializeField] private AirportGameController gameController;
        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] private Text staminaText;
        [SerializeField] private Text moneyText;
        [SerializeField] private Text scoldText;
        [SerializeField] private Text cleaningText;
        [SerializeField] private Text timerText;
        [SerializeField] private Text promptText;
        [SerializeField] private Text managerDialogueText;
        [SerializeField] private Button endDayButton;

        private float managerDialogueHideAt;

        private void Awake()
        {
            if (gameController == null)
            {
                gameController = FindObjectOfType<AirportGameController>();
            }

            if (playerInteractor == null)
            {
                playerInteractor = FindObjectOfType<PlayerInteractor>();
            }
        }

        private void OnEnable()
        {
            if (gameController != null)
            {
                gameController.StateChanged += Refresh;
                gameController.ManagerDialogue += ShowManagerDialogue;
            }

            if (endDayButton != null)
            {
                endDayButton.onClick.AddListener(EndDay);
            }
        }

        private void OnDisable()
        {
            if (gameController != null)
            {
                gameController.StateChanged -= Refresh;
                gameController.ManagerDialogue -= ShowManagerDialogue;
            }

            if (endDayButton != null)
            {
                endDayButton.onClick.RemoveListener(EndDay);
            }
        }

        private void Start()
        {
            Refresh();
        }

        private void Update()
        {
            RefreshPromptAndTimer();

            if (managerDialogueText != null && managerDialogueText.gameObject.activeSelf && Time.time >= managerDialogueHideAt)
            {
                managerDialogueText.gameObject.SetActive(false);
            }
        }

        private void EndDay()
        {
            if (gameController != null)
            {
                gameController.EndDay();
            }
        }

        private void Refresh()
        {
            if (gameController == null || gameController.PlayerProgress == null)
            {
                return;
            }

            PlayerProgress progress = gameController.PlayerProgress;
            JobProgress cleaning = progress.GetJob(JobCategory.Cleaning);

            SetText(staminaText, $"Stamina: {progress.Stamina}");
            SetText(moneyText, $"Money: {progress.Money}");
            SetText(scoldText, $"Scolded: {progress.ScoldCount}");
            SetText(cleaningText, $"Cleaning Lv {cleaning.Level} / Pts {cleaning.Points}");
            RefreshPromptAndTimer();
        }

        private void RefreshPromptAndTimer()
        {
            if (gameController != null)
            {
                SetText(timerText, $"Day: {Mathf.CeilToInt(gameController.TimeRemaining)}s");
            }

            SetText(promptText, playerInteractor != null ? playerInteractor.CurrentPrompt : string.Empty);
        }

        private void ShowManagerDialogue(string line)
        {
            if (managerDialogueText == null)
            {
                return;
            }

            managerDialogueText.text = line;
            managerDialogueText.gameObject.SetActive(true);
            managerDialogueHideAt = Time.time + 4f;
        }

        private static void SetText(Text text, string value)
        {
            if (text != null)
            {
                text.text = value;
            }
        }
    }
}
