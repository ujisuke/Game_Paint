using Assets.Scripts.StageTiles.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class PStatePaint: IPState
    {
        private readonly PlayerModel pM;

        public PStatePaint(PlayerModel playerModel)
        {
            pM = playerModel;
        }

        public void OnStateEnter()
        {
            Debug.Log("PStatePaint");
        }

        public void OnStateFixedUpdate()
        {
            pM.MoveInput();
            StageTilesModel.Instance.PaintTile(pM.PSA.Pos, pM.ColorNameCurrent);
            if (pM.IsDead())
                pM.ChangeState(new PStateDead(pM));
            else if (!Input.GetMouseButton(0))
                pM.ChangeState(new PStateMove(pM));
        }

        public void OnStateExit()
        {
            StageTilesModel.Instance.CompletePaint(pM.ColorNameCurrent);
        }
    }
}