using Assets.Scripts.Datas;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Objects.FamiliarAttacks.Base.View;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.Controller
{
    public class FamiliarAttackController : MonoBehaviour
    {
        [SerializeField] private FamiliarAttackData familiarAttackData;
        private FamiliarAttackModel familiarAttackModel;
        public FamiliarAttackModel FamiliarAttackModel => familiarAttackModel;
        [SerializeField] private FamiliarAttackView familiarAttackView;

        public void Initialize(bool isEnemy, ColorName colorName)
        {
            familiarAttackModel = new FamiliarAttackModel(familiarAttackData, transform.position, this, isEnemy, colorName);
        }

        private void Update()
        {
            if (familiarAttackModel == null)
                return;
            familiarAttackModel.OnUpdate();
            familiarAttackView.SetPSA(familiarAttackModel.PSA);
        }
    }
}
