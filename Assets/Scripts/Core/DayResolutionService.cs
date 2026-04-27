using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportSurvival.Core
{
    public sealed class DayResolutionService
    {
        private readonly int inactivityGraceDays;
        private readonly int decayPerMissedDay;

        public DayResolutionService(int inactivityGraceDays = 2, int decayPerMissedDay = 10)
        {
            if (inactivityGraceDays < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(inactivityGraceDays));
            }

            if (decayPerMissedDay < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(decayPerMissedDay));
            }

            this.inactivityGraceDays = inactivityGraceDays;
            this.decayPerMissedDay = decayPerMissedDay;
        }

        public DayResolutionResult ResolveDay(PlayerProgress player, IEnumerable<JobCategory> practicedJobs)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (practicedJobs == null)
            {
                throw new ArgumentNullException(nameof(practicedJobs));
            }

            HashSet<JobCategory> practiced = practicedJobs.ToHashSet();
            Dictionary<JobCategory, int> decayByJob = new Dictionary<JobCategory, int>();

            foreach (JobProgress job in player.Jobs.Values)
            {
                if (practiced.Contains(job.Category))
                {
                    continue;
                }

                job.MarkNotPracticed();
                int decay = job.ApplyInactivityDecay(inactivityGraceDays, decayPerMissedDay);
                if (decay > 0)
                {
                    decayByJob[job.Category] = decay;
                }
            }

            return new DayResolutionResult(decayByJob, player.CanTakeOverAirport);
        }
    }
}
