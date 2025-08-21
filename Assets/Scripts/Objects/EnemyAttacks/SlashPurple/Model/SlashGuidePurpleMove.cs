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
    public class SlashGuidePurpleMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public SlashGuidePurpleMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new SlashGuidePurpleMove(eAM, eAC);

        public void OnAwake()
        {
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            eAM.SetActiveHitBox(false);
            eAC.PlayAnim("Awake");
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUP("AliveSeconds")), cancellationToken: eAM.Token);
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
