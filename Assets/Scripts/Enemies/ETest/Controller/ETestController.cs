using Assets.Scripts.Enemies.Base.Controller;
using Assets.Scripts.Enemies.FTest.Model;
using UnityEngine;

namespace Assets.Scripts.Enemies.FTest.Controller
{
    public class ETestController : EnemyController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new ETestStateMove(null), pos);

        private void Awake()
        {
            OnSummon(transform.position);
        }
    }
}
