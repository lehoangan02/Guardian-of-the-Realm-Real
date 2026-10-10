namespace GuardianRealm.Hands.Contracts
{
    public interface IHeroPlacementPreview
    {
        bool TryEvaluate(in HeroPlacementQuery query, out HeroPlacementPreview preview);
    }
}
