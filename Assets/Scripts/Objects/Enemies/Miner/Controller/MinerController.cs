using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Miner.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Miner.Controller
{
    public class MinerController : EnemyController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new MinerStateMakeWall(this), pos);

        private void Start()
        {
            OnSummon(transform.position);
        }
    }
}
