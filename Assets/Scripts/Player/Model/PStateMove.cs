using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PStateMove: IPState
    {
        private readonly PlayerModel pM;

        public PStateMove(PlayerModel playerModel)
        {
            pM = playerModel;
        }

        public void OnStateEnter()
        {
            Debug.Log("PStateMove");
        }

        public void OnStateFixedUpdate()
        {
            pM.MoveInput();
            pM.SetColor(Input.mouseScrollDelta);
            if (pM.IsDead())
                pM.ChangeState(new PStateDead(pM));
            else if (Input.GetMouseButton(0))
                pM.ChangeState(new PStatePaint(pM));
        }

        public void OnStateExit()
        {

        }
    }
}