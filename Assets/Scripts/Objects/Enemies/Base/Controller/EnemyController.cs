using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.Enemies.Base.View;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Base.Controller
{
    public abstract class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        private EnemyModel enemyModel;
        [SerializeField] private EnemyView enemyView;

        public abstract void OnSummon(Vector2 pos);

        protected void Initialize(IEStateAfterBorn eStateAfterBorn, Vector2 pos)
        {
            enemyModel = new EnemyModel(enemyData, eStateAfterBorn, pos, this);
            enemyView.SetPSA(enemyModel.PSA);
        }

        private void FixedUpdate()
        {
            if (enemyModel == null)
                return;
            enemyModel.FixedUpdate();
            enemyView.SetPSA(enemyModel.PSA);
        }
    }

}
