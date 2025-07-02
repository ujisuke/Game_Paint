using Assets.Scripts.CommonObject.Model;
using Assets.Scripts.Familiar.Base.Controller;
using Assets.Scripts.Familiar.FTest.Model;

namespace Assets.Scripts.Familiar.Dog.Controller
{
    public class DogController : FamiliarController
    {
        public override void OnSummon(Position position) =>
            Initialize(new FTestStateMove(null, null), position);
    }
}
