using System;

namespace AirportSurvival.Core
{
    public sealed class ReprimandSystem
    {
        private readonly int staminaPenalty;

        public ReprimandSystem(int staminaPenalty)
        {
            if (staminaPenalty < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(staminaPenalty));
            }

            this.staminaPenalty = staminaPenalty;
        }

        public ReprimandResult Scold(PlayerProgress player, string reason)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            player.ApplyScolding(staminaPenalty);
            return new ReprimandResult(player.ScoldCount, player.Stamina, BuildLine(player.ScoldCount, reason));
        }

        private static string BuildLine(int count, string reason)
        {
            string cleanReason = string.IsNullOrWhiteSpace(reason) ? "일을 똑바로 안 해서" : reason.Trim();
            return $"{cleanReason}! 이걸로 {count}번째 혼나는 거야. 일 좀 똑바로 해라.";
        }
    }
}
