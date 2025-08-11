using System.Threading;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Player.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class PlayerModel
    {
        private HP hP;
        private Ink ink;
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
        public PlayerData PlayerData => playerData;
        public CancellationToken Token => token;
        public float HPRatio => hP.Ratio;
        public float InkRatio => ink.Ratio;
        public bool IsInkEmpty => ink.IsEmpty;

        public PlayerModel(PlayerData playerData, Vector2 pos, PlayerController playerController, ColorDataList colorDataList)
        {
            this.playerData = playerData;
            hP = new HP(playerData.MaxHP);
            ink = new Ink();
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
            Vector2 posPrev = playerMove.PA.Pos;
            playerMove.Move(isUp, isDown, isLeft, isRight);
            hurtBox = hurtBox.Move(playerMove.PA.Pos - posPrev);
        }

        public void TakeDamage(float damageValue) => hP = hP.TakeDamage(damageValue);

        public void ReduceInk() => ink = ink.Reduce(playerData.ReduceInkPerSecond * Time.deltaTime);

        public void AddInk() => ink = ink.Add(playerData.AddInkPerSecond * Time.deltaTime);

        public void Heal(float healRate) => hP = hP.Heal(healRate);

        public bool IsDead() => hP.IsDead();
        
        public bool IsLessThanHalfHP() => hP.IsLessThanHalf();

        public void SetActiveHurtBox(bool isactive) => hurtBox = hurtBox.SetActive(isactive);

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemovePlayer();
            GameObject.Destroy(playerController.gameObject);
        }
    }
}
