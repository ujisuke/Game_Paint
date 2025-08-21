using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.MudPurple.Model
{
    public class MudPurpleMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public MudPurpleMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new MudPurpleMove(eAM, eAC);

        public void OnAwake()
        {
            Mud().Forget();
        }

        private async UniTask Mud()
        {
            eAM.SetActiveHitBox(false);
            float appearDelaySeconds = eAM.GetUP("AppearDelaySeconds");
            eAC.PlayAnim("Awake", appearDelaySeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(appearDelaySeconds), cancellationToken: eAM.Token);
            float appearSeconds = eAM.GetUP("AppearSeconds");
            eAC.PlayAnim("Appear");
            eAM.SetActiveHitBox(true);
            await UniTask.Delay(TimeSpan.FromSeconds(appearSeconds), cancellationToken: eAM.Token);
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
