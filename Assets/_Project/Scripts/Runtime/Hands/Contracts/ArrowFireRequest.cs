using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct ArrowFireRequest
    {
        public ArrowFireRequest(
            InteractionContext context,
            Vector3 boardLocalOrigin,
            Vector3 boardLocalAimDirection,
            float normalizedPower,
            Vector3 predictedBoardLocalLandingPoint)
        {
            Context = context;
            BoardLocalOrigin = ContractGuard.Finite(boardLocalOrigin, nameof(boardLocalOrigin));
            BoardLocalAimDirection = ContractGuard.Direction(boardLocalAimDirection, nameof(boardLocalAimDirection));
            NormalizedPower = ContractGuard.UnitInterval(normalizedPower, nameof(normalizedPower));
            PredictedBoardLocalLandingPoint = ContractGuard.Finite(
                predictedBoardLocalLandingPoint,
                nameof(predictedBoardLocalLandingPoint));
        }

        public InteractionContext Context { get; }
        public Vector3 BoardLocalOrigin { get; }
        public Vector3 BoardLocalAimDirection { get; }
        public float NormalizedPower { get; }
        public Vector3 PredictedBoardLocalLandingPoint { get; }
    }
}
