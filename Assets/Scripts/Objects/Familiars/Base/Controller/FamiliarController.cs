using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Model;
using Assets.Scripts.Objects.Familiars.Base.View;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Base.Controller
{
    public abstract class FamiliarController : MonoBehaviour
    {
        [SerializeField] private FamiliarData familiarData;
        private FamiliarModel familiarModel;
        [SerializeField] private FamiliarView familiarView;

        public abstract void OnSummon(Vector2 pos, ColorName colorNameInput, bool isEnemy);

        protected void Initialize(IFStateAfterBorn fStateAfterBorn, Vector2 pos, ColorName colorName, bool isEnemy)
        {
            familiarModel = new FamiliarModel(familiarData, fStateAfterBorn, pos, this, colorName, isEnemy);
            familiarView.SetPA(familiarModel.PA);
        }

        private void Update()
        {
            if (familiarModel == null)
                return;
            familiarModel.OnUpdate();
            familiarView.SetPA(familiarModel.PA);
            familiarView.SetColor(familiarModel.ColorName);
        }
    }

}
