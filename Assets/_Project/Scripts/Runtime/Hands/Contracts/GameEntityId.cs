using System;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct GameEntityId : IEquatable<GameEntityId>
    {
        public GameEntityId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Entity IDs must contain a value.", nameof(value));
            }

            Value = value;
        }

        public string Value { get; }

        public bool Equals(GameEntityId other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

        public override bool Equals(object obj) => obj is GameEntityId other && Equals(other);

        public override int GetHashCode() => Value == null ? 0 : StringComparer.Ordinal.GetHashCode(Value);

        public override string ToString() => Value ?? string.Empty;

        public static bool operator ==(GameEntityId left, GameEntityId right) => left.Equals(right);

        public static bool operator !=(GameEntityId left, GameEntityId right) => !left.Equals(right);
    }
}
