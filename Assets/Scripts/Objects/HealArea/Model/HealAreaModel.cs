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
        private readonly float healRate;
        private readonly HealAreaController healAreaController;
        public HitBox HitBox => hitBox;
        public float HealRate => healRate;

        public HealAreaModel(HealAreaController healAreaController, Vector2 pos, ColorEffectData colorEffectData, float healRate)
        {
            this.healAreaController = healAreaController;
            hitBox = new HitBox(pos, colorEffectData.HealAreaScale);
            this.healRate = healRate;
            ObjectsStorageModel.Instance.AddHealArea(this);
        }

        public void Destroy()
        {
            ObjectsStorageModel.Instance.RemoveHealArea(this);
            GameObject.Destroy(healAreaController.gameObject);
        }
    }
}
