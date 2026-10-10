using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct ThunderStrikeRequest
    {
        public ThunderStrikeRequest(InteractionContext context, Vector3 boardLocalStrikeCenter)
        {
            Context = context;
            BoardLocalStrikeCenter = ContractGuard.Finite(boardLocalStrikeCenter, nameof(boardLocalStrikeCenter));
        }

        public InteractionContext Context { get; }
        public Vector3 BoardLocalStrikeCenter { get; }
    }
}
