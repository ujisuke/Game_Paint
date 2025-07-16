using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BStateWin : IBState
    {
        private readonly BattleSystemModel bSM;

        public BStateWin(BattleSystemModel bSM)
        {
            this.bSM = bSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("BStateWin");
            SceneChangerModel.Instance.LoadSceneMap();
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}