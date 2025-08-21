using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.GameSystems.Camera
{
    public class CameraMove : MonoBehaviour
    {
        private float z;
        private Vector2 max, min;
        private bool isInitialized;

        private void Awake()
        {
            isInitialized = false;
            z = transform.position.z;
        }

        private void LateUpdate()
        {
            if(!isInitialized)
                Initialize();
            Vector2 playerPos = ObjectStorageModel.Instance.GetPlayerPos(transform.position);
            Vector2 targetPos = new(math.clamp(playerPos.x, min.x, max.x), math.clamp(playerPos.y, min.y, max.y));
            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        private void Initialize()
        {
            if (StageData.Instance == null)
                return;
            StageData instance = StageData.Instance;
            min = new Vector2(instance.Width * 0.4f, instance.Height * 0.4f);
            max = new Vector2(instance.Width * 0.6f, instance.Height * 0.6f);
            isInitialized = true;
        }
    }
}
