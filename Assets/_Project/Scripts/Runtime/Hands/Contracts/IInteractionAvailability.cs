namespace GuardianRealm.Hands.Contracts
{
    public interface IInteractionAvailability
    {
        ActionDecision Check(InteractionKind kind, HandSide primaryHand);
    }
}
