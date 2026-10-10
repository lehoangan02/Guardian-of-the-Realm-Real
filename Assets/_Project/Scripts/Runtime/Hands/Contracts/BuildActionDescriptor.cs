namespace GuardianRealm.Hands.Contracts
{
    public readonly struct BuildActionDescriptor
    {
        public BuildActionDescriptor(GameEntityId actionId, string label, bool enabled)
        {
            ActionId = ContractGuard.EntityId(actionId, nameof(actionId));
            Label = label ?? string.Empty;
            Enabled = enabled;
        }

        public GameEntityId ActionId { get; }
        public string Label { get; }
        public bool Enabled { get; }
    }
}
