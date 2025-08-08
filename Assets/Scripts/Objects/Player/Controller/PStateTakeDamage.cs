using System;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Player.Model;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateTakeDamage: IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;
        private bool isDown;

        public PStateTakeDamage(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {
            ObjectsStorageModel.Instance.TakeDamagePlayer();
            pC.PlayAnim("Damage");
            Invinciblize().Forget();
        }

        private async UniTask Invinciblize()
        {
            isDown = true;
            pM.SetActiveHurtBox(false);
            await UniTask.Delay(TimeSpan.FromSeconds(pM.PlayerData.DownSeconds), cancellationToken: pM.Token);
            isDown = false;
            await UniTask.Delay(TimeSpan.FromSeconds(pM.PlayerData.InvincibleSeconds), cancellationToken: pM.Token);
            pM.SetActiveHurtBox(true);
        }

        public void HandleInput()
        {
            pC.PlayerView.SetPA(pM.PA);
            pC.PlayerView.SetPHurtBox(pM.HurtBox);
            if (isDown)
                return;
            else
                pSM.ChangeState(new PStateIdle(pM, pSM, pC));
        }

        public void OnStateExit()
        {
            
        }
    }
}