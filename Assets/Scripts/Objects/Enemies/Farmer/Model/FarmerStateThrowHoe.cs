using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
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
            if(eM.IsLatter)
                summonCount++;
            ThrowHoe().Forget();
        }

        private async UniTask ThrowHoe()
        {
            eC.PlayAnim("Walk");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("ThrowHoeDelaySeconds")), cancellationToken: eM.Token);
            throwHoeSeconds = eM.GetUP("ThrowHoeSeconds");
            int setCount = (int)eM.GetUP("ThrowHoeSetCount");
            int countPerSet = (int)eM.GetUP("ThrowHoeCountPerSet");
            float coolDownSeconds = throwHoeSeconds / setCount - 0.2f * countPerSet;
            for (int i = 0; i < setCount; i++)
            {
                Vector2 throwDir = (ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) - eM.Pos).normalized * 0.5f;
                for (int j = 0; j < countPerSet; j++)
                {
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.Pos + (Vector2)(Quaternion.Euler(0, 0, 45) * throwDir), Quaternion.identity);
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.Pos + throwDir, Quaternion.identity);
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.Pos + (Vector2)(Quaternion.Euler(0, 0, -45) * throwDir), Quaternion.identity);
                    await UniTask.Delay(100, cancellationToken: eM.Token);
                }

                for (int j = 0; j < countPerSet; j++)
                {
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.Pos + (Vector2)(Quaternion.Euler(0, 0, 30) * throwDir), Quaternion.identity);
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.Pos + (Vector2)(Quaternion.Euler(0, 0, -30) * throwDir), Quaternion.identity);
                    await UniTask.Delay(100, cancellationToken: eM.Token);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(coolDownSeconds), cancellationToken: eM.Token);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("ThrowHoeCoolDownSeconds")), cancellationToken: eM.Token);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new FarmerStateThrowPlant(eM, eC, attackCount, summonCount));

        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new FarmerStateDown(eM, eC, attackCount, summonCount));            
            Vector2 playerPos = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos);
            eC.FlipX(playerPos.x < eM.Pos.x);
            eM.MoveInStage((eM.Pos - playerPos).normalized * Time.deltaTime * 0.5f);
        }

        public void OnStateExit()
        {

        }
        
    }
}
