using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct HeroMoveRequest
    {
        public HeroMoveRequest(
            InteractionContext context,
            GameEntityId heroId,
            Vector3 snappedBoardLocalRoadPosition)
        {
            Context = context;
            HeroId = ContractGuard.EntityId(heroId, nameof(heroId));
            SnappedBoardLocalRoadPosition = ContractGuard.Finite(
                snappedBoardLocalRoadPosition,
                nameof(snappedBoardLocalRoadPosition));
        }

        public InteractionContext Context { get; }
        public GameEntityId HeroId { get; }
        public Vector3 SnappedBoardLocalRoadPosition { get; }
    }
}
