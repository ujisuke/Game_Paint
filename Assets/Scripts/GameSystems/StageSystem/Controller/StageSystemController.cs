using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.StageSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.StageSystem.Controller
{
    public class StageSystemController : MonoBehaviour
    {
        [SerializeField] private StagePEDataList stagePEDataList;
        [SerializeField] private SummonDataList signDataList;
        [SerializeField] private StageData stageData;
        private StageSystemModel stageSystemModel;

        private void Awake()
        {
            stageSystemModel = new StageSystemModel(stagePEDataList);
            signDataList.SetInstance();
            stageData.SetInstance();
        }

        private void FixedUpdate()
        {
            stageSystemModel.OnUpdate();
        }
    }
}
