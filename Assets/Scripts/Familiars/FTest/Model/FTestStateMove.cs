using Assets.Scripts.Familiars.Base.Model;
using Assets.Scripts.GameSystems.Model;
using UnityEngine;

namespace Assets.Scripts.Familiars.FTest.Model
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
            targetPos = ObjectStorageModel.Instance.GetNearestEnemyPos(fM.PSA.Pos);
        }

        public void OnStateFixedUpdate()
        {
            fM.Move(fM.FamiliarData.GetUniqueParameter("Speed") * (targetPos - fM.PSA.Pos).normalized);
            if (Vector2.Distance(targetPos, fM.PSA.Pos) < 0.1f)
                fM.ChangeState(new FTestStateAttack(fM));
        }

        public void OnStateExit()
        {

        }
    }
}
