using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.ObjectsStorage.Controller
{
    public class ObjectStorageController : MonoBehaviour
    {
        ObjectsStorageModel objectStorageModel;

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