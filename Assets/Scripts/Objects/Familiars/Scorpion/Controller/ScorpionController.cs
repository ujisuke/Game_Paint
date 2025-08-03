using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Scorpion.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Scorpion.Controller
{
    public class ScorpionController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new ScorpionStateAttack(null, null), pos, colorNameInput, isEnemy);
    }
}
