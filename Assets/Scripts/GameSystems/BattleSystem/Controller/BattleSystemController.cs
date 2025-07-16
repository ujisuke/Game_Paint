using Assets.Scripts.GameSystems.BattleSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.BattleSystem.Controller
{
    public class BattleSystemController : MonoBehaviour
    {
        private BattleSystemModel battleSystemModel;
        
        private void Awake()
        {
            battleSystemModel = new BattleSystemModel();
        }

        private void FixedUpdate()
        {
            battleSystemModel.FixedUpdate();
        }
    }
}
