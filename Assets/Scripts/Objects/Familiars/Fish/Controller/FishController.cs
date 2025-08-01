using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Fish.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Fish.Controller
{
    public class FishController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new FishStateAttack(null, null), pos, colorNameInput, isEnemy);
    }
}
