namespace Assets.Scripts.Player.Model
{
    public class PStateDead : IPState
    {
        private readonly PlayerModel pM;

        public PStateDead(PlayerModel playerModel) => pM = playerModel;

        public void OnStateEnter()
        {
            pM.Destroy();
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}