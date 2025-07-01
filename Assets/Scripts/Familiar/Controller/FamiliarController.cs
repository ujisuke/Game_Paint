using Assets.Scripts.Datas;
using Assets.Scripts.Familiar.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.Controller
{
    public class FamiliarController : MonoBehaviour
    {
        [SerializeField] private FamiliarData familiarData;
        FamiliarModel familiarModel;

        private void Awake()
        {
            familiarModel = new FamiliarModel(familiarData);
        }

        private void FixedUpdate()
        {
            if (familiarModel == null)
                return;
            familiarModel.FixedUpdate();
        }
    }

}
