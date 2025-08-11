using System.Threading;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Player.Controller;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

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
        private readonly float reloadInkSeconds;
        public PA PA => playerMove.PA;
        public HurtBox HurtBox => hurtBox;
        public ColorName ColorNameCurrent => playerColor.ColorNameCurrent;
        public PlayerData PlayerData => playerData;
        public CancellationToken Token => token;
        public float HPRatio => hP.Ratio;
        public float InkRatio => ink.Ratio;
        public bool IsInkEmpty => ink.IsEmpty;
        public bool IsInkReloading => ink.IsReloading;


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
            reloadInkSeconds = playerData.ReloadInkSeconds;
        }

        public void SetColor(float mouseScrollDelta) => playerColor.SetColor(mouseScrollDelta);
        
        public void MoveInput(bool isUp, bool isDown, bool isLeft, bool isRight)
        {
            playerMove.Move(isUp, isDown, isLeft, isRight);
            hurtBox = hurtBox.SetPos(playerMove.PA.Pos);
        }

        public void TakeDamage(float damageValue) => hP = hP.TakeDamage(damageValue);

        public void ReduceInk() => ink = ink.Reduce(playerData.ReduceInkPerSecond * Time.deltaTime);

        public async UniTask ReloadInk()
        {
            if (ink.IsReloading || ink.IsFull)
                return;
            float reloadInkSecondsUnit = reloadInkSeconds * 0.01f;
            ink = Ink.BeginReload();
            for (int i = 0; i < 100; i++)
            {
                ink = ink.Add(0.01f);
                await UniTask.Delay(TimeSpan.FromSeconds(reloadInkSecondsUnit), cancellationToken: token);
            }
            ink = Ink.EndReload();
        }

        public void Heal(float healRate) => hP = hP.Heal(healRate);

        public bool IsDead() => hP.IsDead();

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
