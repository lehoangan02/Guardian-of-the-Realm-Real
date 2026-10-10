namespace GuardianRealm.Hands.Contracts
{
    public interface IArrowTrajectoryPreview
    {
        bool TryPredict(in ArrowPreviewQuery query, out ArrowTrajectoryPreview preview);
    }
}
