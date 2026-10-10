namespace GuardianRealm.Hands.Contracts
{
    public interface IInteractionFeedback
    {
        void Began(in InteractionContext context, InteractionKind kind);
        void Cancelled(in InteractionContext context, InteractionCancellationReason reason);
        void Resolved(in InteractionContext context, in ActionDecision decision);
    }
}
