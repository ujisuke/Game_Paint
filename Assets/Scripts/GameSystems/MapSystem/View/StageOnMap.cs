using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.View
{
    public class StageOnMap : MonoBehaviour
    {
        [SerializeField] private string stageSceneName;
        [SerializeField] private SpriteRenderer stageSpriteRenderer;
        public string StageSceneName => stageSceneName;

        public void Select()
        {
            stageSpriteRenderer.color = Color.white;
        }

        public void Deselect()
        {
            stageSpriteRenderer.color = Color.gray;
        }
    }
}
