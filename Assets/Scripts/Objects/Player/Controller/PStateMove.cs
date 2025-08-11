using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Player.Model;
using Assets.Scripts.UI.PlayerStatus.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateMove: IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;
        private bool isTryingPaint;

        public PStateMove(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {
            isTryingPaint = Input.GetMouseButton(0);
            pC.PlayAnim("Walk");
        }

        public void HandleInput()
        {
            if (isTryingPaint && !Input.GetMouseButton(0))
                isTryingPaint = false;
            pM.MoveInput(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
            pC.PlayerView.SetPA(pM.PA);
            pC.PlayerView.SetPHurtBox(pM.HurtBox);
            pM.SetColor(Input.mouseScrollDelta.y);
            pC.PlayerView.SetColor(pM.ColorNameCurrent);
            if (Input.GetMouseButton(1))
                pM.ReloadInk().Forget();
            PlayerStatusView.Instance.SetInkBar(pM.InkRatio);
            if (Input.GetKey(KeyCode.A))
                pC.FlipX(true);
            else if (Input.GetKey(KeyCode.D))
                pC.FlipX(false);

            if (pM.IsDead())
                pSM.ChangeState(new PStateDead(pM, pSM, pC));
            else if (ObjectsStorageModel.Instance.IsPlayerTakingDamage())
                pSM.ChangeState(new PStateTakeDamage(pM, pSM, pC));
            else if (Input.GetMouseButton(0) && !isTryingPaint && !pM.IsInkEmpty && !pM.IsInkReloading)
                pSM.ChangeState(new PStatePaint(pM, pSM, pC));
            else if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                pSM.ChangeState(new PStateIdle(pM, pSM, pC));
        }

        public void OnStateExit()
        {

        }
    }
}