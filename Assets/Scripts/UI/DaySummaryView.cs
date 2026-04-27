using System.Text;
using AirportSurvival.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace AirportSurvival.UI
{
    public sealed class DaySummaryView : MonoBehaviour
    {
        [SerializeField] private AirportGameController gameController;
        [SerializeField] private GameObject panel;
        [SerializeField] private Text summaryText;

        private void Awake()
        {
            if (gameController == null)
            {
                gameController = FindObjectOfType<AirportGameController>();
            }

            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        private void OnEnable()
        {
            if (gameController != null)
            {
                gameController.DayEnded += ShowSummary;
            }
        }

        private void OnDisable()
        {
            if (gameController != null)
            {
                gameController.DayEnded -= ShowSummary;
            }
        }

        private void ShowSummary(AirportDaySummary summary)
        {
            if (panel != null)
            {
                panel.SetActive(true);
            }

            if (summaryText == null)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Day Complete");
            builder.AppendLine($"Trash cleaned: {summary.CleanedTrash}");
            builder.AppendLine($"Cleaning points: +{summary.CleaningPoints}");
            builder.AppendLine($"Money earned: +{summary.MoneyEarned}");
            builder.AppendLine($"Stamina remaining: {summary.StaminaRemaining}");
            builder.AppendLine($"Scolded: {summary.ScoldCount}");

            foreach (var decay in summary.DecayByJob)
            {
                builder.AppendLine($"{decay.Key} decay: -{decay.Value}");
            }

            summaryText.text = builder.ToString();
        }
    }
}
