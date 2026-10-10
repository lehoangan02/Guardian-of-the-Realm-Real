using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct HeroPlacementQuery
    {
        public HeroPlacementQuery(GameEntityId heroId, Vector3 candidateBoardLocalPosition)
        {
            HeroId = ContractGuard.EntityId(heroId, nameof(heroId));
            CandidateBoardLocalPosition = ContractGuard.Finite(
                candidateBoardLocalPosition,
                nameof(candidateBoardLocalPosition));
        }

        public GameEntityId HeroId { get; }
        public Vector3 CandidateBoardLocalPosition { get; }
    }
}
