using Assets.Scripts.Common;
using Assets.Scripts.Familiar.Base.Controller;
using Assets.Scripts.Familiar.FTest.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.FTest.Controller
{
    public class FTestController : FamiliarController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new FTestStateMove(null), pos);
    }
}
