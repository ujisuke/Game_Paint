using Assets.Scripts.Datas;
using Assets.Scripts.Objects.ObjectAttacks.Base.Model;
using Assets.Scripts.Objects.ObjectAttacks.Base.View;
using UnityEngine;

namespace Assets.Scripts.Objects.ObjectAttacks.Base.Controller
{
    public class ObjectAttackController : MonoBehaviour
    {
        [SerializeField] private ObjectAttackData objectAttackData;
        private ObjectAttackModel objectAttackModel;
        public ObjectAttackModel ObjectAttackModel => objectAttackModel;
        [SerializeField] private ObjectAttackView objectAttackView;

        private void Awake()
        {
            objectAttackModel = new ObjectAttackModel(objectAttackData, transform.position, this);
            Debug.Log("FTestAttack Awake");
        }

        private void FixedUpdate()
        {
            if (objectAttackModel == null)
                return;
            objectAttackModel.FixedUpdate();
            objectAttackView.SetPSA(objectAttackModel.PSA);
        }
    }
}
