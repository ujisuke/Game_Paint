using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Scoop.Model
{
    public class ScoopMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private float moveSpeed;

        public ScoopMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new ScoopMove(eAM, eAC);

        public void OnAwake()
        {
            Vector2 enemyPos = ObjectsStorageModel.Instance.GetHostilePos(eAM.PA.Pos, false);
            moveDir = (eAM.PA.Pos - enemyPos).normalized;
            moveSpeed = eAM.GetUniqueParameter("MoveSpeed");
            eAC.PlayAnim("Awake");
            eAM.Rotate(Vector2.SignedAngle(Vector2.right, moveDir));
        }

        public void OnUpdate()
        {
            eAM.MoveIgnoringStage(moveSpeed * Time.deltaTime * moveDir);
            if (StageData.Instance.IsOutOfStage(eAM.PA.Pos))
                eAM.Destroy();
        }
    }
}
