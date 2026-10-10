namespace GuardianRealm.Hands.Contracts
{
    public enum ActionRejectionCode
    {
        None = 0,
        Unavailable = 1,
        InvalidTarget = 2,
        Cooldown = 3,
        InvalidPlacement = 4,
        InsufficientResources = 5,
        Duplicate = 6,
        Cancelled = 7,
        Unknown = 255
    }
}
