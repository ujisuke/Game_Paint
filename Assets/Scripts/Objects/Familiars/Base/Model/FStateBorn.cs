using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public class FStateBorn : IFState
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private readonly IFStateAfterBorn fStateAfterBorn;
        private float seconds;

        public FStateBorn(FamiliarModel fM, FamiliarController fC, IFStateAfterBorn fStateAfterBorn)
        {
            this.fM = fM;
            this.fC = fC;
            this.fStateAfterBorn = fStateAfterBorn;
            seconds = 0f;
        }

        public void OnStateEnter()
        {
            fC.PlayAnim("Born", 0.3f);
            Vector2 targetPos = ObjectsStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
        }

        public void OnUpdate()
        {
            seconds += Time.deltaTime;
            if (seconds >= 0.3f)
                fM.ChangeState(fStateAfterBorn.Initialize(fM, fC));
        }

        public void OnStateExit()
        {

        }
    }
}