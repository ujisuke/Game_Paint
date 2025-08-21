using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Model
{
    public class SlashMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public SlashMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new SlashMove(eAM, eAC);

        public void OnAwake()
        {
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUP("AliveSeconds")), cancellationToken: eAM.Token);
            eAM.Destroy();
        }

        public void OnUpdate()
        {
            Vector2 enemyPos = ObjectStorageModel.Instance.GetEnemyPos(eAM.Pos);
            eAM.MoveIgnoringStage(enemyPos - eAM.Pos);
        }
    }
}
