using Assets.Scripts.Datas;
using Assets.Scripts.Familiars.Base.Controller;
using Assets.Scripts.Familiars.FTest.Model;
using UnityEngine;

namespace Assets.Scripts.Familiars.FTest.Controller
{
    public class FTestController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput) =>
            Initialize(new FTestStateMove(null), pos, colorNameInput);
    }
}
