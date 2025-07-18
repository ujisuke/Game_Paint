using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.BattleSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.BattleSystem.Controller
{
    public class BattleSystemController : MonoBehaviour
    {
        [SerializeField] private BattlePEDataList battlePEDataList;
        private BattleSystemModel battleSystemModel;
        
        private void Awake()
        {
            battleSystemModel = new BattleSystemModel(battlePEDataList);
        }

        private void FixedUpdate()
        {
            battleSystemModel.FixedUpdate();
        }
    }
}
