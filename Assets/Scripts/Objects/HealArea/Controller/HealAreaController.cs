using Assets.Scripts.Datas;
using Assets.Scripts.Objects.HealArea.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.HealArea.Controller
{
    public class HealAreaController : MonoBehaviour
    {
        [SerializeField] private HealAreaData healAreaData;
        private HealAreaModel healAreaModel;

        private void Awake()
        {
            healAreaModel = new HealAreaModel(this, transform.position, healAreaData);
        }
    }
}
