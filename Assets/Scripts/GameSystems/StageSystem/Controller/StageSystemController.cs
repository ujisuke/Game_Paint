using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.StageSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.StageSystem.Controller
{
    public class StageSystemController : MonoBehaviour
    {
        [SerializeField] private StagePEDataList stagePEDataList;
        [SerializeField] private SignDataList signDataList;
        private StageSystemModel stageSystemModel;

        private void Awake()
        {
            stageSystemModel = new StageSystemModel(stagePEDataList);
            signDataList.SetInstance();
        }

        private void FixedUpdate()
        {
            stageSystemModel.OnUpdate();
        }
    }
}
