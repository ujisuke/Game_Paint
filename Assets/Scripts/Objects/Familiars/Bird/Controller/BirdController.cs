using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Bird.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Bird.Controller
{
    public class BirdController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new BirdStateAttack(null, null), pos, colorNameInput, isEnemy);
    }
}
