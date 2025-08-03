using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public interface IFStateAfterBorn : IFState
    {
        IFState Initialize(FamiliarModel fM, FamiliarController fC);
    }
}
