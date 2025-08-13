using System;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Miner.Model
{
    public class MinerStateThrowBomb : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public MinerStateThrowBomb(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            ThrowBomb().Forget();
        }

        private async UniTask ThrowBomb()
        {
            eC.PlayAnim("ThrowBomb");
            float throwBombSeconds = eM.GetUP("ThrowBombSeconds");

            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Bomb"), eM.PA.Pos + new Vector2(1f, 2f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Bomb"), eM.PA.Pos + new Vector2(-1f, -2f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Bomb"), eM.PA.Pos + new Vector2(-3f, 1f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Bomb"), eM.PA.Pos + new Vector2(3f, -1f), Quaternion.identity);

            await UniTask.Delay(TimeSpan.FromSeconds(throwBombSeconds - 0.3f), cancellationToken: eM.Token);    

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new MinerStateRideCart(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new MinerStateSummon(eM, eC, attackCount, summonCount));
            else if (attackCount == 0)
                eM.ChangeState(new MinerStateMakeWall(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new MinerStateJam(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new MinerStateDown(eM, eC, attackCount, summonCount));
            eC.FlipX(ObjectStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
