using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Hoe.Model
{
    public class HoeMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private float moveSpeed;

        public HoeMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new HoeMove(eAM, eAC);

        public void OnAwake()
        {
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eAM.PA.Pos, true);
            Vector2 targetPos = playerPos + Random.insideUnitCircle * eAM.GetUniqueParameter("RandomRange");
            moveDir = (targetPos - eAM.PA.Pos).normalized;
            moveSpeed = eAM.GetUniqueParameter("MoveSpeed");
            eAC.PlayAnim("Awake");
            eAM.Rotate(Vector2.SignedAngle(Vector2.right, moveDir));
        }

        public void OnUpdate()
        {
            eAM.Move(moveSpeed * Time.deltaTime * moveDir);
            if (StageData.Instance.IsOutOfStage(eAM.PA.Pos))
                eAM.Destroy();
        }
    }
}
