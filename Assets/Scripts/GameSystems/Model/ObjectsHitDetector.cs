using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.GameSystems.Model
{
    public class ObjectsHitDetector
    {
        public static bool IsAttacking(HitBox hitBox, HurtBox hurtBox)
        {
            if (!hurtBox.IsActive)
                return false;

            Vector2 hitBoxSize = hitBox.Size * 0.5f;
            Vector2 hurtBoxSize = hurtBox.Size * 0.5f;
            Vector2 hitBoxPos = hitBox.Pos;
            Vector2 hurtBoxPos = hurtBox.Pos;

            return hitBoxPos.x + hitBoxSize.x > hurtBoxPos.x - hurtBoxSize.x &&
                   hitBoxPos.x - hitBoxSize.x < hurtBoxPos.x + hurtBoxSize.x &&
                   hitBoxPos.y + hitBoxSize.y > hurtBoxPos.y - hurtBoxSize.y &&
                   hitBoxPos.y - hitBoxSize.y < hurtBoxPos.y + hurtBoxSize.y;
        }
    }
}
