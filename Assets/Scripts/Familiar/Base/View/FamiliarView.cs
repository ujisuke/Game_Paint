using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Familiar.Base.View
{
    public class FamiliarView : MonoBehaviour
    {
        public void SetPSA(PSA pSA)
        {
            transform.position = pSA.Pos;
            transform.localScale = pSA.Scale;
            transform.rotation = Quaternion.Euler(0f, 0f, pSA.Angle);
        }
    }
}
