using Assets.Scripts.CommonObject.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Familiar.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.Base.Controller
{
    public abstract class FamiliarController : MonoBehaviour
    {
        [SerializeField] private FamiliarData familiarData;
        private FamiliarModel familiarModel;

        public abstract void OnSummon(Position position);

        protected void Initialize(IFStateAfterBorn fStateAfterBorn, Position position) =>
            familiarModel = new FamiliarModel(familiarData, fStateAfterBorn, position);

        private void FixedUpdate()
        {
            if (familiarModel == null)
                return;
            familiarModel.FixedUpdate();
        }
    }

}
