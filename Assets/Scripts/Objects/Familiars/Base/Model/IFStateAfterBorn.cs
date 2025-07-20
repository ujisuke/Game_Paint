namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public interface IFStateAfterBorn : IFState
    {
        IFState Initialize(FamiliarModel familiarModel);
    }
}
