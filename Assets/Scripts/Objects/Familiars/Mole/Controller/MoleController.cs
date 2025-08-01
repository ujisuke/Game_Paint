using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Mole.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Mole.Controller
{
    public class MoleController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new MoleStateAttack(null, null), pos, colorNameInput, isEnemy);
    }
}
