namespace GuardianRealm.Hands.Contracts
{
    public interface IGameInput
    {
        ActionDecision RequestArrow(in ArrowFireRequest request);
        ActionDecision RequestThunder(in ThunderStrikeRequest request);
        ActionDecision RequestMeteor(in MeteorDropRequest request);
        ActionDecision RequestHeroMove(in HeroMoveRequest request);
        ActionDecision RequestBuildAction(in BuildActionRequest request);
    }
}
