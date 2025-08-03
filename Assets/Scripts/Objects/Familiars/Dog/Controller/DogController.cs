using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Objects.Familiars.Dog.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Dog.Controller
{
    public class DogController : FamiliarController
    {
        public override void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy) =>
            Initialize(new DogStateAttack(null, null), pos, colorNameInput, isEnemy);
    }
}
