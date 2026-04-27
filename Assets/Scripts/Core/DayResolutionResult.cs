using System.Collections.Generic;

namespace AirportSurvival.Core
{
    public readonly struct DayResolutionResult
    {
        public DayResolutionResult(IReadOnlyDictionary<JobCategory, int> decayByJob, bool canTakeOverAirport)
        {
            DecayByJob = decayByJob;
            CanTakeOverAirport = canTakeOverAirport;
        }

        public IReadOnlyDictionary<JobCategory, int> DecayByJob { get; }
        public bool CanTakeOverAirport { get; }
    }
}
