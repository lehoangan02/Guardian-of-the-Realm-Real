using UnityEngine;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct MeteorDropRequest
    {
        public MeteorDropRequest(
            InteractionContext context,
            Vector3 boardLocalLandingPoint,
            Vector3 boardLocalHandOrigin)
        {
            Context = context;
            BoardLocalLandingPoint = ContractGuard.Finite(boardLocalLandingPoint, nameof(boardLocalLandingPoint));
            BoardLocalHandOrigin = ContractGuard.Finite(boardLocalHandOrigin, nameof(boardLocalHandOrigin));
        }

        public InteractionContext Context { get; }
        public Vector3 BoardLocalLandingPoint { get; }
        public Vector3 BoardLocalHandOrigin { get; }
    }
}
