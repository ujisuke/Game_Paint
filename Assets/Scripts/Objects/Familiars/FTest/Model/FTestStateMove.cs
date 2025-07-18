using Assets.Scripts.Objects.Familiars.Base.Model;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.FTest.Model
{
    public class FTestStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private Vector2 targetPos;

        public FTestStateMove(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateMove");
            targetPos = ObjectsStorageModel.Instance.GetNearestEnemyPos(fM.PSA.Pos);
        }

        public void OnStateFixedUpdate()
        {
            fM.Move(fM.FamiliarData.GetUniqueParameter("Speed") * (targetPos - fM.PSA.Pos).normalized);
            if (fM.IsDead())
                fM.ChangeState(new FStateDead(fM));
            else if (Vector2.Distance(targetPos, fM.PSA.Pos) < 0.1f)
                fM.ChangeState(new FTestStateAttack(fM));
        }

        public void OnStateExit()
        {

        }
    }
}
