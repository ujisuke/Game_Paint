using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.FTest.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.FTest.Controller
{
    public class FTestController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new FTestStateMove(null), pos, colorNameInput, isEnemy);
    }
}
