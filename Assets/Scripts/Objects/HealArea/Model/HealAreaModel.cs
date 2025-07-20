using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;
using Assets.Scripts.Objects.HealArea.Controller;

namespace Assets.Scripts.Objects.HealArea.Model
{
    public class HealAreaModel
    {
        private readonly HitBox hitBox;
        private readonly int healAmount;
        private readonly HealAreaController healAreaController;
        public HitBox HitBox => hitBox;
        public int HealAmount => healAmount;

        public HealAreaModel(HealAreaController healAreaController, Vector2 pos, HealAreaData healAreaData)
        {
            this.healAreaController = healAreaController;
            hitBox = new HitBox(pos, healAreaData.HitBoxScale);
            healAmount = healAreaData.HealAmount;
            ObjectsStorageModel.Instance.AddHealArea(this);
        }

        public void Destroy()
        {
            ObjectsStorageModel.Instance.RemoveHealArea(this);
            GameObject.Destroy(healAreaController.gameObject);
        }
    }
}
