using Assets.Scripts.Objects.Familiars.Base.Model;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;
using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.FTest.Model
{
    public class FTestStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private Vector2 targetPos;
        private int i;

        public FTestStateMove(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel, FamiliarController fC) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateMove");
            targetPos = ObjectsStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            i = 0;
        }

        public void OnUpdate()
        {
            fM.Move(fM.FamiliarData.GetUniqueParameter("Speed") * (targetPos - fM.PA.Pos).normalized);
            i++;
            if (i >= 50)
                fM.ChangeState(new FStateDead(fM));
            else if (Vector2.Distance(targetPos, fM.PA.Pos) < 0.1f)
                fM.ChangeState(new FTestStateAttack(fM));
        }

        public void OnStateExit()
        {

        }
    }
}
