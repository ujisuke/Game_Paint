using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Plant.Model
{
    public class PlantMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private float moveDist;
        private float seconds;
        private float airborneSeconds;
        private bool isGrowing;


        public PlantMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new PlantMove(eAM, eAC);

        public void OnAwake()
        {
            seconds = 0f;
            isGrowing = false;
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eAM.PA.Pos, true);
            moveDir = (playerPos - eAM.PA.Pos).normalized;
            moveDist = Vector2.Distance(playerPos, eAM.PA.Pos);
            airborneSeconds = eAM.GetUniqueParameter("AirborneSeconds");
            eAC.PlayAnim("Awake");
            eAC.FlipX(playerPos.x < eAM.PA.Pos.x);
            eAM.SetActiveHitBox(false);
        }

        public void OnUpdate()
        {
            if (isGrowing)
                return;
            float secondsPrev = seconds;
            seconds += Time.deltaTime;
            Vector2 moveDirNext = Vector2.up * (math.sin(seconds / airborneSeconds * math.PI) - math.sin(secondsPrev / airborneSeconds * math.PI)) + moveDist * Time.deltaTime * moveDir / airborneSeconds;
            eAM.MoveIgnoringStage(moveDirNext);
            if (seconds < airborneSeconds)
                return;
            eAC.PlayAnim("Grow");
            isGrowing = true;
            eAM.SetActiveHitBox(true);
        }
    }
}
