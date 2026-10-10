using System;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct InteractionContext
    {
        public InteractionContext(InteractionId id, HandSide primaryHand, double timestampSeconds)
        {
            if (id.Value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Interaction IDs must be non-zero.");
            }

            if (double.IsNaN(timestampSeconds) || double.IsInfinity(timestampSeconds) || timestampSeconds < 0d)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampSeconds));
            }

            Id = id;
            PrimaryHand = primaryHand;
            TimestampSeconds = timestampSeconds;
        }

        public InteractionId Id { get; }

        public HandSide PrimaryHand { get; }

        public double TimestampSeconds { get; }
    }
}
