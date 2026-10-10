namespace GuardianRealm.Hands.Contracts
{
    public enum InteractionCancellationReason
    {
        UserCancelled = 0,
        TrackingLost = 1,
        Paused = 2,
        Superseded = 3,
        InvalidTarget = 4
    }
}
