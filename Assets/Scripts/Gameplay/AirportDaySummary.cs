using System.Collections.Generic;
using AirportSurvival.Core;

namespace AirportSurvival.Gameplay
{
    public sealed class AirportDaySummary
    {
        public AirportDaySummary(
            int cleanedTrash,
            int cleaningPoints,
            int moneyEarned,
            int staminaRemaining,
            int scoldCount,
            IReadOnlyDictionary<JobCategory, int> decayByJob)
        {
            CleanedTrash = cleanedTrash;
            CleaningPoints = cleaningPoints;
            MoneyEarned = moneyEarned;
            StaminaRemaining = staminaRemaining;
            ScoldCount = scoldCount;
            DecayByJob = decayByJob;
        }

        public int CleanedTrash { get; }
        public int CleaningPoints { get; }
        public int MoneyEarned { get; }
        public int StaminaRemaining { get; }
        public int ScoldCount { get; }
        public IReadOnlyDictionary<JobCategory, int> DecayByJob { get; }
    }
}
