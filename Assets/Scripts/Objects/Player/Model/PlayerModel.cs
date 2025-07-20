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
        private HP hP;
        private HurtBox hurtBox;
        private readonly PlayerData playerData;
        private readonly PlayerController playerController;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        private readonly PlayerMove playerMove;
        private readonly PlayerColor playerColor;
        public PA PA => playerMove.PA;
        public HurtBox HurtBox => hurtBox;
        public ColorName ColorNameCurrent => playerColor.ColorNameCurrent;

        public PlayerModel(PlayerData playerData, Vector2 pos, PlayerController playerController, ColorDataList colorDataList)
        {
            this.playerData = playerData;
            hP = playerData.MaxHP;
            this.playerController = playerController;
            playerMove = new PlayerMove(playerData.MoveSpeed, playerData.Scale, pos, 0f);
            hurtBox = new HurtBox(playerMove.PA.Pos, playerData.HurtBoxScale, true);
            playerColor = new PlayerColor(colorDataList);
            ObjectsStorageModel.Instance.AddPlayer(this);
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        public void SetColor(float mouseScrollDelta) => playerColor.SetColor(mouseScrollDelta);
        
        public void MoveInput(bool isUp, bool isDown, bool isLeft, bool isRight)
        {
            playerMove.Move(isUp, isDown, isLeft, isRight);
            hurtBox = hurtBox.Move(playerMove.PA.Pos);
        }

        public async UniTask TakeDamage(int damageValue)
        {
            hP = hP.TakeDamage(damageValue);
            hurtBox = hurtBox.Inactivate();
            await UniTask.Delay(playerData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.Activate();
        }

        public void Heal(float healRate) => hP = hP.Heal(healRate);

        public bool IsDead() => hP.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemovePlayer();
            GameObject.Destroy(playerController.gameObject);
        }
    }
}
