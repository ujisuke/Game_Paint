using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Assets.Scripts.Objects.Familiars.Squid.Model
{
    public class SquidStateAttack : IFState
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private readonly List<FamiliarAttackModel> attackList;
        private float seconds;
        private Vector2 targetPos;

        public SquidStateAttack(FamiliarModel fM, FamiliarController fC, Vector2 targetPos)
        {
            this.fM = fM;
            this.fC = fC;
            this.targetPos = targetPos;
            seconds = 0f;
            attackList = new();
        }

        public void OnStateEnter()
        {
            Vector2 attackDir = (targetPos - fM.PA.Pos).normalized * math.min(fM.FamiliarData.HitBoxScale.x, fM.FamiliarData.HitBoxScale.y);
            for (int i = 0; i < (targetPos - fM.PA.Pos).magnitude / math.min(fM.FamiliarData.HitBoxScale.x, fM.FamiliarData.HitBoxScale.y); i++)
            {
                var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
                var fAC = newAttack.GetComponent<FamiliarAttackController>();
                fAC.Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
                var attack = fAC.FamiliarAttackModel;
                attack.Move(attackDir * i);
                attackList.Add(attack);
            }
            fC.PlayAnim("Attack");
        }

        public void OnUpdate()
        {
            seconds += Time.deltaTime;
            if (seconds >= fM.FamiliarData.GetUniqueParameter("AttackSeconds"))
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            for(int i = 0; i < attackList.Count; i++)
                attackList[i]?.Destroy();
        }
    }
}
