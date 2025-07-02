using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.Familiar.Base.Controller;
using Assets.Scripts.Familiar.FTest.Model;

namespace Assets.Scripts.Familiar.Dog.Controller
{
    public class DogController : FamiliarController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new FTestStateMove(null), pos);
    }
}
