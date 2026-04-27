namespace AirportSurvival.Core
{
    public readonly struct ReprimandResult
    {
        public ReprimandResult(int scoldCount, int staminaAfterPenalty, string dialogueLine)
        {
            ScoldCount = scoldCount;
            StaminaAfterPenalty = staminaAfterPenalty;
            DialogueLine = dialogueLine;
        }

        public int ScoldCount { get; }
        public int StaminaAfterPenalty { get; }
        public string DialogueLine { get; }
    }
}
