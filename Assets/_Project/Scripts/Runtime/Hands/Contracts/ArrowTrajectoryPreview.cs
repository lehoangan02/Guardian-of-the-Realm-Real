using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct ArrowTrajectoryPreview
    {
        public ArrowTrajectoryPreview(Vector3 boardLocalLandingPoint, bool isValid)
        {
            BoardLocalLandingPoint = ContractGuard.Finite(boardLocalLandingPoint, nameof(boardLocalLandingPoint));
            IsValid = isValid;
        }

        public Vector3 BoardLocalLandingPoint { get; }
        public bool IsValid { get; }
    }
}
