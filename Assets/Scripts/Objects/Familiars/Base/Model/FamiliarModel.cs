using System.Threading;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public class FamiliarModel
    {
        private PA pA;
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarData familiarData;
        private readonly FamiliarController familiarController;
        private readonly bool isEnemy;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        private readonly ColorName colorName;
        private readonly GameObject attackPrefab;

        public PA PA => pA;
        public ColorName ColorName => colorName;
        public FamiliarData FamiliarData => familiarData;
        public bool IsEnemy => isEnemy;
        public GameObject AttackPrefab => attackPrefab;

        public FamiliarModel(FamiliarData familiarData, IFStateAfterBorn fStateAfterBorn, Vector2 pos, FamiliarController familiarController, ColorName colorName, bool isEnemy, GameObject attackPrefab)
        {
            this.familiarData = familiarData;
            pA = new PA(pos, 0f);
            fStateMachine = new FStateMachine(this, familiarController, fStateAfterBorn);
            this.familiarController = familiarController;
            this.colorName = colorName;
            this.isEnemy = isEnemy;
            cts = new CancellationTokenSource();
            token = cts.Token;
            this.attackPrefab = attackPrefab;
        }

        public void OnUpdate()
        {
            fStateMachine.OnUpdate();
        }

        public void Move(Vector2 dir)
        {
            pA = pA.Move(dir);
        }
        
        public void ChangeState(IFState state) => fStateMachine.ChangeState(state);

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            GameObject.Destroy(familiarController.gameObject);
        }
    }
}
