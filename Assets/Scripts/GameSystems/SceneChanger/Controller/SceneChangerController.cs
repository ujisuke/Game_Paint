using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.SceneChanger.Controller
{
    public class SceneChangerController : MonoBehaviour
    {
        [SerializeField] private string sceneNameBattle;
        private bool isPushed = false;

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space) && !isPushed)
            {
                SceneChangerModel.Instance.LoadSceneBattle(sceneNameBattle, false);
                isPushed = true;
            }
        }
    }
}
