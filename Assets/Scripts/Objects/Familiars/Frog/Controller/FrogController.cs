using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Frog.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Frog.Controller
{
    public class FrogController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new FrogStateUp(null, null), pos, colorNameInput, isEnemy);
    }
}
