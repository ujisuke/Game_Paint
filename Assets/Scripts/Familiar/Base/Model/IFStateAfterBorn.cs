namespace Assets.Scripts.Familiar.Base.Model
{
    public interface IFStateAfterBorn : IFState
    {
        IFState Initialize(FStateMachine fStateMachine, FamiliarModel familiarModel);
    }
}
