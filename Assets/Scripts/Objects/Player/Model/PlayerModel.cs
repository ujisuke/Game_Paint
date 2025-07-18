using System.Threading;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Player.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class PlayerModel
    {
        private PSA pSA;
        public PSA PSA => pSA;
        private HP hP;
        private HurtBox hurtBox;
        public HurtBox HurtBox => hurtBox;
        private readonly PStateMachine pStateMachine;
        private readonly PlayerData playerData;
        private readonly PlayerController playerController;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        private PlayerMove playerMove;
        private PlayerColor playerColor;
        public ColorName ColorNameCurrent => playerColor.ColorNameCurrent;

        public PlayerModel(PlayerData playerData, Vector2 position, PlayerController playerController)
        {
            this.playerData = playerData;
            pSA = new PSA(position, playerData.Scale, 0f);
            hP = playerData.MaxHP;
            hurtBox = new HurtBox(pSA.Pos, playerData.HurtBoxScale, true);
            pStateMachine = new PStateMachine(this);
            this.playerController = playerController;
            playerMove = PlayerMove.Initialize(playerData.MoveSpeed, playerData.HurtBoxScale);
            playerColor = PlayerColor.Initialize();
            ObjectsStorageModel.Instance.AddPlayer(this);
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        public void SetColor(Vector2 mouseScrollDelta)
        {
            playerColor = playerColor.SetColor(mouseScrollDelta);
        }

        public void FixedUpdate() => pStateMachine.FixedUpdate();

        public void Move(Vector2 dir)
        {
            pSA = pSA.Move(dir);
            hurtBox = hurtBox.Move(pSA.Pos);
        }

        public void MoveInput()
        {
            playerMove = playerMove.Move(
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D),
                pSA.Pos
            );
            Move(playerMove.DirectionVector);
        }

        public void ChangeState(IPState state) => pStateMachine.ChangeState(state);

        public async UniTask TakeDamage(int damageValue)
        {
            hP = hP.TakeDamage(damageValue);
            hurtBox = hurtBox.Inactivate();
            await UniTask.Delay(playerData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.Activate();
        }

        public bool IsDead() => hP.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemovePlayer(this);
            GameObject.Destroy(playerController.gameObject);
        }
    }
}
