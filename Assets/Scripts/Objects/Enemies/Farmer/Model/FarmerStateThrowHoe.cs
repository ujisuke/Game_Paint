using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateThrowHoe : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private float restSeconds;
        private float throwHoeSeconds;

        public FarmerStateThrowHoe(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
        }

        public void OnStateEnter()
        {
            eC.PlayAnim("Walk");
            throwHoeSeconds = eM.GetUP("ThrowHoeSeconds");
            restSeconds = throwHoeSeconds + eM.GetUP("ThrowHoeCoolDownSeconds");
            ThrowHoe().Forget();
        }

        private async UniTask ThrowHoe()
        {
            int count = (int)eM.GetUP("ThrowHoeCount");
            int coolDownMilliseconds = (int)(throwHoeSeconds / count * 1000);
            for (int i = 0; i < count; i++)
            {
                await UnityEngine.Object.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Hoe"), eM.PA.Pos, Quaternion.identity);
                await UniTask.Delay(coolDownMilliseconds, cancellationToken: eM.Token);
            }
        }

        public void OnUpdate()
        {
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (restSeconds <= 0)
                if (Vector2.Distance(eM.PA.Pos, playerPos) <= eM.GetUP("NearDistance") || StageData.Instance.IsOutOfStage(eM.PA.Pos))
                    eM.ChangeState(new FarmerStateJumpBegin(eM, eC));
                else
                    if (Random.Range(0, 2) == 0)
                        eM.ChangeState(new FarmerStateThrowPlant(eM, eC));
                    else
                        eM.ChangeState(new FarmerStateThrowHoe(eM, eC));
            
            eC.FlipX(playerPos.x < eM.PA.Pos.x);
            restSeconds -= Time.deltaTime;
            eM.Move((eM.PA.Pos - playerPos).normalized * Time.deltaTime);
        }

        public void OnStateExit()
        {

        }
        
    }
}
