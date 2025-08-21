using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Bomb.Model
{
    public class BombMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        public BombMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new BombMove(eAM, eAC);

        public void OnAwake()
        {
            Vector2 enemyPos = ObjectStorageModel.Instance.GetEnemyPos(eAM.Pos);
            Vector2 playerPos = ObjectStorageModel.Instance.GetHostilePos(eAM.Pos, true);
            moveDir = (playerPos - enemyPos) * 0.01f;
            Move().Forget();
        }

        private async UniTask Move()
        {
            eAC.PlayAnim("Awake");
            eAM.SetActiveHitBox(false);
            for (int i = 0; i < 100; i++)
            {
                eAM.MoveIgnoringStage(0.05f * math.cos(0.01f * math.PI * i) * Vector2.up);
                eAM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(0.002f));
            }
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUP("ExplosionDelaySeconds") - 0.6f));
            eAC.PlayAnim("BeforeExplosion", 1f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
            eAC.PlayAnim("Explosion");
            eAM.SetActiveHitBox(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            eAM.Destroy();
        }


        public void OnUpdate()
        {

        }
    }
}
