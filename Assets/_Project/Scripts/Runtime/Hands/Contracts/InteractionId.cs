using System;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct InteractionId : IEquatable<InteractionId>
    {
        public InteractionId(ulong value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Interaction IDs must be non-zero.");
            }

            Value = value;
        }

        public ulong Value { get; }

        public bool Equals(InteractionId other) => Value == other.Value;

        public override bool Equals(object obj) => obj is InteractionId other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();

        public static bool operator ==(InteractionId left, InteractionId right) => left.Equals(right);

        public static bool operator !=(InteractionId left, InteractionId right) => !left.Equals(right);
    }
}
