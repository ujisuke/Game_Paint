using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Human.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Human.Controller
{
    public class HumanController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new HumanStatePaint(null, null), pos, colorNameInput, isEnemy);
    }
}
