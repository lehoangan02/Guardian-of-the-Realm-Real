namespace GuardianRealm.Hands.Contracts
{
    public readonly struct BuildActionRequest
    {
        public BuildActionRequest(
            InteractionContext context,
            GameEntityId buildSpotId,
            GameEntityId actionId)
        {
            Context = context;
            BuildSpotId = ContractGuard.EntityId(buildSpotId, nameof(buildSpotId));
            ActionId = ContractGuard.EntityId(actionId, nameof(actionId));
        }

        public InteractionContext Context { get; }
        public GameEntityId BuildSpotId { get; }
        public GameEntityId ActionId { get; }
    }
}
