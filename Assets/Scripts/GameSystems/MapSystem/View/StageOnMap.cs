using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.View
{
    public class StageOnMap : MonoBehaviour
    {
        [SerializeField] private string stageSceneName;
        [SerializeField] private SpriteRenderer stageSpriteRenderer;
        [SerializeField] private Animator animator;
        public string StageSceneName => stageSceneName;

        public void Select()
        {
            stageSpriteRenderer.color = Color.white;
            animator.Play("Selected");
        }

        public void Deselect()
        {
            stageSpriteRenderer.color = Color.gray;
            animator.Play("Deselected");
        }
    }
}
