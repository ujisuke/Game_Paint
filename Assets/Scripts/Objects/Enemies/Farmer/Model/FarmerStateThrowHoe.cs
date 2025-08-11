using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateThrowHoe : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private float throwHoeSeconds;
        private int attackCount;
        private int summonCount;

        public FarmerStateThrowHoe(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount++;
            if(eM.IsLessThanHalfHP())
                summonCount++;
            ThrowHoe().Forget();
        }

        private async UniTask ThrowHoe()
        {
            eC.PlayAnim("Walk");
            throwHoeSeconds = eM.GetUP("ThrowHoeSeconds");
            int count = (int)eM.GetUP("ThrowHoeCount");
            float coolDownSeconds = throwHoeSeconds / count;
            for (int i = 0; i < count; i++)
            {
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.PA.Pos, Quaternion.identity);
                await UniTask.Delay(TimeSpan.FromSeconds(coolDownSeconds), cancellationToken: eM.Token);
            }
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
            else if (StageData.Instance.IsOutOfStage(eM.PA.Pos))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
            else if (Vector2.Distance(eM.PA.Pos, ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true)) <= eM.GetUP("NearDistance"))
                if (UnityEngine.Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateSwingScythe(eM, eC, attackCount, summonCount));
            else
                if (UnityEngine.Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateThrowPlant(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC, attackCount, summonCount));

        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            eC.FlipX(playerPos.x < eM.PA.Pos.x);
            eM.Move((eM.PA.Pos - playerPos).normalized * Time.deltaTime);
        }

        public void OnStateExit()
        {

        }
        
    }
}
