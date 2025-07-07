namespace Assets.Scripts.Familiars.Base.Model
{
    public interface IFStateAfterBorn : IFState
    {
        IFState Initialize(FamiliarModel familiarModel);
    }
}
