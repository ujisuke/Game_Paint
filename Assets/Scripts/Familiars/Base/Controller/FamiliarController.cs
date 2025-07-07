using Assets.Scripts.Datas;
using Assets.Scripts.Familiars.Base.Model;
using Assets.Scripts.Familiars.Base.View;
using UnityEngine;

namespace Assets.Scripts.Familiars.Base.Controller
{
    public abstract class FamiliarController : MonoBehaviour
    {
        [SerializeField] private FamiliarData familiarData;
        private FamiliarModel familiarModel;
        [SerializeField] private FamiliarView familiarView;

        public abstract void OnSummon(Vector2 pos, ColorName colorNameInput);

        protected void Initialize(IFStateAfterBorn fStateAfterBorn, Vector2 pos, ColorName colorName)
        {
            familiarModel = new FamiliarModel(familiarData, fStateAfterBorn, pos, this, colorName);
            familiarView.SetPSA(familiarModel.PSA);
        }

        private void FixedUpdate()
        {
            if (familiarModel == null)
                return;
            familiarModel.FixedUpdate();
            familiarView.SetPSA(familiarModel.PSA);
            familiarView.SetColor(familiarModel.ColorName);
        }
    }

}
