using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.FTest.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.FTest.Controller
{
    public class ETestController : EnemyController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new ETestStateMove(null), pos);

        private void Start()
        {
            OnSummon(transform.position);
        }
    }
}
