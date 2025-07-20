using Assets.Scripts.Datas;
using Assets.Scripts.Objects.HealArea.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.HealArea.Controller
{
    public class HealAreaController : MonoBehaviour
    {
        [SerializeField] private ColorEffectData colorEffectData;
        private HealAreaModel healAreaModel;

        public void Initialize(float healRate) => healAreaModel = new HealAreaModel(this, transform.position, colorEffectData, healRate);
    }
}
