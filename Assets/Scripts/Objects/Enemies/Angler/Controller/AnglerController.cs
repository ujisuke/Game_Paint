using Assets.Scripts.Objects.Enemies.Angler.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Angler.Controller
{
    public class AnglerController : EnemyController
    {
        public override void OnSummon(Vector2 pos) =>
            Initialize(new AnglerStateSetShark(this), pos);

        private void Start()
        {
            OnSummon(transform.position);
        }
    }
}
