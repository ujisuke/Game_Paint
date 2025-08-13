using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Objects.Familiars.Human.Model
{
    public class HumanStatePaint : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private float seconds;

        public HumanStatePaint(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
            seconds = 0f;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new HumanStatePaint(fM, fC);

        public void OnStateEnter()
        {
            var targetPos = ObjectStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
            fC.PlayAnim("Paint");

            SummonDataList.Instance.SummonAtRandom(fM.PA.Pos + Vector2.left, fM.ColorName, fM.IsEnemy);
            SummonDataList.Instance.SummonAtRandom(fM.PA.Pos + Vector2.right, fM.ColorName, fM.IsEnemy);
        }

        public void OnUpdate()
        {
            seconds += Time.deltaTime;
            if (seconds >= fM.FamiliarData.GetUP("Seconds"))
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {

        }
    }
}
