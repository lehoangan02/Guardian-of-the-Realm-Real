using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct HeroPlacementPreview
    {
        public HeroPlacementPreview(Vector3 snappedBoardLocalPosition, bool isValid)
        {
            SnappedBoardLocalPosition = ContractGuard.Finite(
                snappedBoardLocalPosition,
                nameof(snappedBoardLocalPosition));
            IsValid = isValid;
        }

        public Vector3 SnappedBoardLocalPosition { get; }
        public bool IsValid { get; }
    }
}
