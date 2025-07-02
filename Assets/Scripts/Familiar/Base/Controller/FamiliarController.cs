using Assets.Scripts.Datas;
using Assets.Scripts.Familiar.Base.Model;
using Assets.Scripts.Familiar.Base.View;
using UnityEngine;

namespace Assets.Scripts.Familiar.Base.Controller
{
    public abstract class FamiliarController : MonoBehaviour
    {
        [SerializeField] private FamiliarData familiarData;
        private FamiliarModel familiarModel;
        [SerializeField] private FamiliarView familiarView;

        public abstract void OnSummon(Vector2 pos);

        protected void Initialize(IFStateAfterBorn fStateAfterBorn, Vector2 pos)
        {
            familiarModel = new FamiliarModel(familiarData, fStateAfterBorn, pos, this);
            familiarView.SetPSA(familiarModel.PSA);
        }

        private void FixedUpdate()
        {
            if (familiarModel == null)
                return;
            familiarModel.FixedUpdate();
            familiarView.SetPSA(familiarModel.PSA);
        }
    }

}
