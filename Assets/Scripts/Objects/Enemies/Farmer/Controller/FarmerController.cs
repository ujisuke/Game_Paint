using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Farmer.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Controller
{
    public class FarmerController : EnemyController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new FarmerStateJump(this), pos);

        private void Start()
        {
            OnSummon(transform.position);
        }
    }
}
