using Assets.Scripts.Objects.Familiars.Base.Model;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.FTest.Model
{
    public class FTestStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private Vector2 targetPos;
        private int i;

        public FTestStateMove(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateMove");
            targetPos = ObjectsStorageModel.Instance.GetNearestEnemyPos(fM.PA.Pos);
            i = 0;
        }

        public void OnUpdate()
        {
            fM.Move(fM.FamiliarData.GetUniqueParameter("Speed") * (targetPos - fM.PA.Pos).normalized);
            if (i < 10)
                i++;
            else if (fM.ColorName == Datas.ColorName.green)
            {
                i = 0;
                GameObject.Instantiate(fM.FamiliarData.HealAreaPrefab, fM.PA.Pos, Quaternion.identity);
            }
            if (fM.IsDead())
                fM.ChangeState(new FStateDead(fM));
            else if (Vector2.Distance(targetPos, fM.PA.Pos) < 0.1f)
                fM.ChangeState(new FTestStateAttack(fM));
        }

        public void OnStateExit()
        {

        }
    }
}
