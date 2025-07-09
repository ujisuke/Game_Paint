using Assets.Scripts.Datas;
using Assets.Scripts.ObjectAttacks.Base.Model;
using Assets.Scripts.ObjectAttacks.Base.View;
using UnityEngine;

namespace Assets.Scripts.ObjectAttacks.Base.Controller
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
