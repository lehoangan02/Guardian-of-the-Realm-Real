using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct ArrowPreviewQuery
    {
        public ArrowPreviewQuery(Vector3 boardLocalOrigin, Vector3 boardLocalAimDirection, float normalizedPower)
        {
            BoardLocalOrigin = ContractGuard.Finite(boardLocalOrigin, nameof(boardLocalOrigin));
            BoardLocalAimDirection = ContractGuard.Direction(boardLocalAimDirection, nameof(boardLocalAimDirection));
            NormalizedPower = ContractGuard.UnitInterval(normalizedPower, nameof(normalizedPower));
        }

        public Vector3 BoardLocalOrigin { get; }
        public Vector3 BoardLocalAimDirection { get; }
        public float NormalizedPower { get; }
    }
}
