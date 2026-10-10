namespace GuardianRealm.Hands.Contracts
{
    public interface IBuildActionCatalog
    {
        int GetActionCount(GameEntityId buildSpotId);
        BuildActionDescriptor GetAction(GameEntityId buildSpotId, int index);
    }
}
