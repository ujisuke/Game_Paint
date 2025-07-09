using Assets.Scripts.GameSystems.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.Controller
{
    public class ObjectStorageController : MonoBehaviour
    {
        ObjectStorageModel objectStorageModel;

        private void Awake()
        {
            objectStorageModel = new();
        }

        private void FixedUpdate()
        {
            objectStorageModel.DetectHit();
        }
    }
}