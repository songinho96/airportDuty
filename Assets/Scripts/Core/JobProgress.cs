using System;

namespace AirportSurvival.Core
{
    [Serializable]
    public sealed class JobProgress
    {
        public JobProgress(JobCategory category, int maxLevel = 5, int pointsPerLevel = 100)
        {
            Category = category;
            MaxLevel = maxLevel;
            PointsPerLevel = pointsPerLevel;
        }

        public JobCategory Category { get; }
        public int Points { get; private set; }
        public int DaysSincePracticed { get; private set; }
        public int MaxLevel { get; }
        public int PointsPerLevel { get; }
        public int Level => Math.Min(MaxLevel, Points / PointsPerLevel);
        public bool IsMaxLevel => Level >= MaxLevel;

        public void AddPoints(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            Points += amount;
            DaysSincePracticed = 0;
        }

        public void MarkNotPracticed()
        {
            DaysSincePracticed++;
        }

        public int ApplyInactivityDecay(int graceDays, int pointsPerMissedDay)
        {
            if (graceDays < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(graceDays));
            }

            if (pointsPerMissedDay < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pointsPerMissedDay));
            }

            if (DaysSincePracticed < graceDays)
            {
                return 0;
            }

            int decay = (DaysSincePracticed - graceDays + 1) * pointsPerMissedDay;
            int applied = Math.Min(Points, decay);
            Points -= applied;
            return applied;
        }
    }
}
