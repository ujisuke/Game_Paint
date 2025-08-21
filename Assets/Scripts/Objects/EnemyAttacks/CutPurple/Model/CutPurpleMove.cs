using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.CutPurple.Model
{
    public class CutPurpleMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public CutPurpleMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new CutPurpleMove(eAM, eAC);

        public void OnAwake()
        {
            Cut().Forget();
        }

        private async UniTask Cut()
        {
            eAM.SetActiveHitBox(false);
            Vector2 posVector = eAM.Pos - ObjectStorageModel.Instance.GetEnemyPos(eAM.Pos);
            float angle = Vector2.SignedAngle(Vector2.right, posVector);
            eAM.Rotate(angle - 45f);
            float cutDelaySeconds = eAM.GetUP("CutDelaySeconds");
            eAC.PlayAnim("Awake", cutDelaySeconds * 0.6f);
            await UniTask.Delay(TimeSpan.FromSeconds(cutDelaySeconds), cancellationToken: eAM.Token);
            float cutSeconds = eAM.GetUP("CutSeconds");
            eAC.PlayAnim("Cut", cutSeconds);
            eAM.SetActiveHitBox(true);
            await UniTask.Delay(TimeSpan.FromSeconds(cutSeconds), cancellationToken: eAM.Token);
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
