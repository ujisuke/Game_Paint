using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.EnemyStatus.View
{
    public class EnemyStatusView : MonoBehaviour
    {
        [SerializeField] private Image hPBar;
        private static EnemyStatusView instance;
        public static EnemyStatusView Instance => instance;

        private void Awake()
        {
            instance = this;
            SetHPBar(1f);
        }

        public void SetHPBar(float hPRatio)
        {
            hPBar.fillAmount = hPRatio;
        }

    }
}
