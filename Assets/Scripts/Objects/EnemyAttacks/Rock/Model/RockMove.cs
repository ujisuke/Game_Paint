using System;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Objects.EnemyAttacks.Rock.Model
{
    public class RockMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public RockMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new RockMove(eAM, eAC);

        public void OnAwake()
        {
            Instantiate().Forget();
        }

        private async UniTask Instantiate()
        {
            float noticeSeconds = eAM.GetUP("NoticeSeconds");
            eAC.PlayAnim("Notice", noticeSeconds);
            eAM.SetActiveHitBox(false);
            await UniTask.Delay(TimeSpan.FromSeconds(noticeSeconds), cancellationToken: eAM.Token);
            eAC.PlayAnim("Awake");
            eAM.SetActiveHitBox(true);
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUP("ExistSeconds")), cancellationToken: eAM.Token);
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
