using System;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Scythe.Model
{
    public class ScytheMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private Vector2 pivotPos;

        public ScytheMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new ScytheMove(eAM, eAC);

        public void OnAwake()
        {
            eAC.PlayAnim("Awake");
            eAM.Rotate(Vector2.SignedAngle(Vector2.right, new Vector2(1f, 1f)));
            pivotPos = eAM.PA.Pos;
            eAM.MoveIgnoringStage(new Vector2(1f, 1f).normalized * eAM.GetUniqueParameter("Range"));
            Rotate().Forget();
        }

        private async UniTask Rotate()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUniqueParameter("SwingDelaySeconds")), cancellationToken: eAM.Token);
            float swingSecondsDelta = eAM.GetUniqueParameter("SwingSeconds") / 36f;
            moveDir = (Vector2)(Quaternion.Euler(0f, 0f, -10f) * (eAM.PA.Pos - pivotPos)) - (eAM.PA.Pos - pivotPos);
            for (int i = 0; i < 36; i++)
            {
                eAM.Rotate(-10f);
                eAM.MoveIgnoringStage(moveDir);
                moveDir = Quaternion.Euler(0f, 0f, -10f) * moveDir;
                await UniTask.Delay(TimeSpan.FromSeconds(swingSecondsDelta), cancellationToken: eAM.Token);
            }
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
