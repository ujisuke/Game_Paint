using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Player.Model;
using Assets.Scripts.UI.PlayerStatus.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateIdle: IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;
        private bool isTryingPaint;

        public PStateIdle(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {
            isTryingPaint = Input.GetMouseButton(0);
            pC.PlayAnim("Idle");
        }

        public void HandleInput()
        {
            pM.SetColor(Input.mouseScrollDelta.y);
            pC.PlayerView.SetColor(pM.ColorNameCurrent);
            PlayerStatusView.Instance.SetInkBar(pM.InkRatio);
            if (Input.GetMouseButton(1))
                pM.ReloadInk().Forget();
            if (isTryingPaint && !Input.GetMouseButton(0))
                isTryingPaint = false;
            if (pM.IsDead())
                pSM.ChangeState(new PStateDead(pM, pSM, pC));
            else if (ObjectStorageModel.Instance.IsPlayerTakingDamage())
                pSM.ChangeState(new PStateTakeDamage(pM, pSM, pC));
            else if (Input.GetMouseButton(0) && !isTryingPaint && !pM.IsInkEmpty && !pM.IsInkReloading)
                pSM.ChangeState(new PStatePaint(pM, pSM, pC));
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                pSM.ChangeState(new PStateMove(pM, pSM, pC));
        }

        public void OnStateExit()
        {

        }
    }
}