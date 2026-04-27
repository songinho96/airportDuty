using AirportSurvival.Core;
using NUnit.Framework;

namespace AirportSurvival.Tests
{
    public sealed class CoreRulesTests
    {
        [Test]
        public void ScoldingIncrementsCountAndReducesStamina()
        {
            PlayerProgress player = new PlayerProgress(startingStamina: 100);
            ReprimandSystem reprimands = new ReprimandSystem(staminaPenalty: 15);

            ReprimandResult result = reprimands.Scold(player, "쓰레기를 안 치웠잖아");

            Assert.AreEqual(1, player.ScoldCount);
            Assert.AreEqual(85, player.Stamina);
            Assert.AreEqual(1, result.ScoldCount);
            StringAssert.Contains("1번째", result.DialogueLine);
        }

        [Test]
        public void PracticedJobGainsPointsAndResetsInactivity()
        {
            PlayerProgress player = new PlayerProgress();

            player.GetJob(JobCategory.Restaurant).MarkNotPracticed();
            player.AddJobPoints(JobCategory.Restaurant, 40);

            Assert.AreEqual(40, player.GetJob(JobCategory.Restaurant).Points);
            Assert.AreEqual(0, player.GetJob(JobCategory.Restaurant).DaysSincePracticed);
        }

        [Test]
        public void IgnoredJobsDecayAfterGraceDays()
        {
            PlayerProgress player = new PlayerProgress();
            DayResolutionService dayResolution = new DayResolutionService(inactivityGraceDays: 2, decayPerMissedDay: 10);

            player.AddJobPoints(JobCategory.Guidance, 50);

            dayResolution.ResolveDay(player, new[] { JobCategory.Cleaning });
            DayResolutionResult result = dayResolution.ResolveDay(player, new[] { JobCategory.Cleaning });

            Assert.AreEqual(40, player.GetJob(JobCategory.Guidance).Points);
            Assert.AreEqual(10, result.DecayByJob[JobCategory.Guidance]);
        }

        [Test]
        public void AirportTakeoverRequiresEveryJobAtMaxLevel()
        {
            PlayerProgress player = new PlayerProgress(maxLevel: 1, pointsPerLevel: 10);

            player.AddJobPoints(JobCategory.Cleaning, 10);
            player.AddJobPoints(JobCategory.Restaurant, 10);
            player.AddJobPoints(JobCategory.CheckIn, 10);
            player.AddJobPoints(JobCategory.Guidance, 10);

            Assert.IsFalse(player.CanTakeOverAirport);

            player.AddJobPoints(JobCategory.Security, 10);

            Assert.IsTrue(player.CanTakeOverAirport);
        }
    }
}
