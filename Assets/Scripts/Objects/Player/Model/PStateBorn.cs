using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class PStateBorn : IPState
    {
        private readonly PlayerModel pM;

        public PStateBorn(PlayerModel playerModel)
        {
            pM = playerModel;
        }

        public void OnStateEnter()
        {
            Debug.Log("PStateBorn");
        }

        public void OnStateFixedUpdate()
        {
            pM.ChangeState(new PStateMove(pM));
        }

        public void OnStateExit()
        {

        }
    }
}