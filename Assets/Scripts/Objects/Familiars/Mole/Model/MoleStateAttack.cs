using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Objects.Familiars.Mole.Model
{
    public class MoleStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private FamiliarAttackModel attack;
        private Vector2 moveDir;

        public MoleStateAttack(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new MoleStateAttack(fM, fC);

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            float hostilePosX = ObjectStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy).x;
            moveDir = (new Vector2(hostilePosX, fM.PA.Pos.y) - fM.PA.Pos).normalized * fM.FamiliarData.GetUP("Speed") * Time.deltaTime;
            fC.FlipX(hostilePosX - fM.PA.Pos.x < 0f);
            attack.MoveIgnoringStage(moveDir.normalized);
            fC.PlayAnim("Attack");
        }

        public void OnUpdate()
        {
            fM.MoveIgnoringStage(moveDir);
            attack.MoveIgnoringStage(moveDir);
            if (StageData.Instance.IsOutOfStage(fM.PA.Pos))
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
