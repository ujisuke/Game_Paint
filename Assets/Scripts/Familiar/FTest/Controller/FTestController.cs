using Assets.Scripts.CommonObject.Model;
using Assets.Scripts.Familiar.Base.Controller;
using Assets.Scripts.Familiar.FTest.Model;

namespace Assets.Scripts.Familiar.FTest.Controller
{
    public class FTestController : FamiliarController
    {
        public override void OnSummon(Position position) =>
            Initialize(new FTestStateMove(null, null), position);
    }
}
