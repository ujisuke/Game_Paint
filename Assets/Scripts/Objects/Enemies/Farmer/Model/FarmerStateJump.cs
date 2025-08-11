using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateJump : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private readonly int attackCount;
        private readonly int summonCount;

        public FarmerStateJump(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public FarmerStateJump(EnemyController enemyController)
        {
            eM = null;
            eC = enemyController;
            attackCount = 0;
            summonCount = 0;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new FarmerStateJump(enemyModel, enemyController, 0, 0);

        public void OnStateEnter()
        {
            Jump().Forget();
        }

        private async UniTask Jump()
        {
            eC.PlayAnim("JumpBegin");
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            eC.PlayAnim("Jumping");
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            Vector2 targetPos = StageData.Instance.CalcRandomPosFarFrom(playerPos);
            Vector2 moveDir = (targetPos - eM.PA.Pos) / 100f;
            float jumpSecondsDelta = eM.GetUP("JumpSeconds") / 100f;
            for (int i = 0; i < 100; i++)
            {
                eM.Move(moveDir);
                await UniTask.Delay(System.TimeSpan.FromSeconds(jumpSecondsDelta), cancellationToken: eM.Token);
            }
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
            else if (StageData.Instance.IsOutOfStage(eM.PA.Pos))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
            else
                if (Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateThrowPlant(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            eC.FlipX(ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
