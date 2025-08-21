using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Controller;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Miner.Model
{
    public class MinerStateJam : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public MinerStateJam(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            Jam().Forget();
        }

        private async UniTask Jam()
        {
            eC.PlayAnim("Whistle");
            int cartCount = (int)eM.GetUP("CartCount");
            float cartIntervalSeconds = eM.GetUP("JamSeconds") / cartCount;
            for (int i = 0; i < cartCount; i++)
            {
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Cart"), StageData.Instance.CalcRandomPosOnEdgeOfStage(), Quaternion.identity);
                await UniTask.Delay(System.TimeSpan.FromSeconds(cartIntervalSeconds), cancellationToken: eM.Token);
            }
            eC.PlayAnim("HideBegin");
            eM.SetHurtBoxActive(false);
            float hidecoolDownSeconds = eM.GetUP("JamCoolDownSeconds");
            await UniTask.Delay(System.TimeSpan.FromSeconds(hidecoolDownSeconds * 0.75f), cancellationToken: eM.Token);
            Vector2 playerPos = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos);
            float targetPosY = playerPos.y > StageData.Instance.StageCenterPos.y ? 2f : StageData.Instance.StageEdgePosMax.y - 2f;
            Vector2 targetPos = new(playerPos.x, targetPosY);
            eM.MoveIgnoringStage(targetPos - eM.Pos);
            eM.SetHurtBoxActive(true);
            eC.PlayAnim("HideEnd", hidecoolDownSeconds * 0.25f);
            await UniTask.Delay(System.TimeSpan.FromSeconds(hidecoolDownSeconds * 0.25f), cancellationToken: eM.Token);
            
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new MinerStateRideCart(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new MinerStateSummon(eM, eC, attackCount, summonCount));
            else if (attackCount == 0)
                eM.ChangeState(new MinerStateMakeWall(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new MinerStateThrowBomb(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new MinerStateDown(eM, eC, attackCount, summonCount));
            eC.FlipX(ObjectStorageModel.Instance.GetPlayerPos(eM.Pos).x < eM.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
