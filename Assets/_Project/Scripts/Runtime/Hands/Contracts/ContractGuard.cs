using System;
using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    internal static class ContractGuard
    {
        public static Vector3 Finite(Vector3 value, string parameterName)
        {
            if (!IsFinite(value.x) || !IsFinite(value.y) || !IsFinite(value.z))
            {
                throw new ArgumentOutOfRangeException(parameterName, "Board-local vectors must be finite.");
            }

            return value;
        }

        public static Vector3 Direction(Vector3 value, string parameterName)
        {
            Finite(value, parameterName);
            if (value.sqrMagnitude <= 0.000001f)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Directions must be non-zero.");
            }

            return value.normalized;
        }

        public static float UnitInterval(float value, string parameterName)
        {
            if (float.IsNaN(value) || float.IsInfinity(value) || value < 0f || value > 1f)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Values must be within 0..1.");
            }

            return value;
        }

        public static GameEntityId EntityId(GameEntityId value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value.Value))
            {
                throw new ArgumentException("Entity IDs must contain a value.", parameterName);
            }

            return value;
        }

        private static bool IsFinite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
    }
}
