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

        public abstract void OnSummon(Vector2 pos, ColorName colorNameInput);

        protected void Initialize(IFStateAfterBorn fStateAfterBorn, Vector2 pos, ColorName colorName)
        {
            familiarModel = new FamiliarModel(familiarData, fStateAfterBorn, pos, this, colorName);
            familiarView.SetPSA(familiarModel.PSA);
        }

        private void Update()
        {
            if (familiarModel == null)
                return;
            familiarModel.OnUpdate();
            familiarView.SetPSA(familiarModel.PSA);
            familiarView.SetColor(familiarModel.ColorName);
        }
    }

}
