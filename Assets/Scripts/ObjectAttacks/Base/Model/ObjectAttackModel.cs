using Assets.Scripts.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.ObjectAttacks.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.ObjectAttacks.Base.Model
{
    public class ObjectAttackModel
    {
        private Power power;
        public int PowerValue => power.CurrentPower;
        private PSA pSA;
        public PSA PSA => pSA;
        private HitBox hitBox;
        public HitBox HitBox => hitBox;
        private readonly ObjectAttackData objectAttackData;
        private readonly ObjectAttackController objectAttackController;

        public ObjectAttackModel(ObjectAttackData objectAttackData, Vector2 position, ObjectAttackController objectAttackController)
        {
            this.objectAttackData = objectAttackData;
            power = objectAttackData.DefaultPower;
            pSA = new PSA(position, objectAttackData.HitBoxScale, 0f);
            hitBox = new(pSA.Pos, objectAttackData.HitBoxScale);
            this.objectAttackController = objectAttackController;
            ObjectsStorageModel.Instance.AddObjectAttack(this, objectAttackData.IsEnemyAttack);
        }

        public void Move(Vector2 dir) => pSA = pSA.Move(dir);

        public void FixedUpdate()
        {
            hitBox = hitBox.Move(pSA.Pos);
        }

        public float GetUniqueParameter(string key) => objectAttackData.GetUniqueParameter(key);

        public void Destroy()
        {
            ObjectsStorageModel.Instance.RemoveObjectAttack(this);
            Object.Destroy(objectAttackController.gameObject);
        }
    }
}
