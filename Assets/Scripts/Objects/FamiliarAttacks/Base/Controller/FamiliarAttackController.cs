using Assets.Scripts.Datas;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Objects.FamiliarAttacks.Base.View;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.Controller
{
    public class FamiliarAttackController : MonoBehaviour
    {
        private FamiliarAttackModel familiarAttackModel;
        public FamiliarAttackModel FamiliarAttackModel => familiarAttackModel;
        [SerializeField] private FamiliarAttackView familiarAttackView;
        [SerializeField] private ColorEffectData colorEffectData;

        public void Initialize(FamiliarData familiarData, bool isEnemy, ColorName colorName)
        {
            familiarAttackModel = new FamiliarAttackModel(familiarData, transform.position, this, isEnemy, colorName, colorEffectData);
        }

        private void Update()
        {
            if (familiarAttackModel == null)
                return;
            familiarAttackModel.OnUpdate();
            familiarAttackView.SetPA(familiarAttackModel.PA);
        }
    }
}
