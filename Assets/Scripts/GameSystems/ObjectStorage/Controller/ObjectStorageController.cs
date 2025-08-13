using Assets.Scripts.GameSystems.ObjectStorage.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.ObjectStorage.Controller
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