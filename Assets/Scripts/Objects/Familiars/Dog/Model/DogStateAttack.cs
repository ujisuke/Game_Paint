using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.Dog.Model
{
    public class DogStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private FamiliarAttackModel attack;
        private Vector2 targetPos;
        private float attackSeconds;
        private float seconds;

        public DogStateAttack(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
            seconds = 0f;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new DogStateAttack(fM, fC);

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            targetPos = fM.PA.Pos + (ObjectsStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy) - fM.PA.Pos).normalized * fM.FamiliarData.GetUniqueParameter("Range");
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
            attackSeconds = fM.FamiliarData.GetUniqueParameter("Seconds");
            fC.PlayAnim("Attack", attackSeconds);
        }

        public void OnUpdate()
        {
            if (seconds < attackSeconds * 0.25f)
            {
                fM.Move(8f * Time.deltaTime * (targetPos - fM.PA.Pos) / attackSeconds / 3f);
                attack.Move(fM.PA.Pos - attack.PA.Pos);
            }
            seconds += Time.deltaTime;
            if (seconds >= attackSeconds)
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
