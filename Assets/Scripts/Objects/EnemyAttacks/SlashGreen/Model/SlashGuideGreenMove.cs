using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashGreen.Model
{
    public class SlashGuideGreenMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public SlashGuideGreenMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new SlashGuideGreenMove(eAM, eAC);

        public void OnAwake()
        {
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            eAM.SetActiveHitBox(false);
            eAC.PlayAnim("Awake");
            eAM.Rotate(90f);
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUP("AliveSeconds")), cancellationToken: eAM.Token);
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
