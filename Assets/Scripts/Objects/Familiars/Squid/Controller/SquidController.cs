using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Squid.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Squid.Controller
{
    public class SquidController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new SquidStateCharge(null, null), pos, colorNameInput, isEnemy);
    }
}
