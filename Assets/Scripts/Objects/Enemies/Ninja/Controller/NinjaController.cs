using Assets.Scripts.Objects.Enemies.Ninja.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Ninja.Controller
{
    public class NinjaController : EnemyController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new NinjaStateCut(this), pos);

        private void Start()
        {
            OnSummon(transform.position);
        }
    }
}
