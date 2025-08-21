using Assets.Scripts.Objects.Common.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.ObjectStorage.Model
{
    public class ObjectHitDetector
    {
        public static bool IsAttacking(HitBox hitBox, HurtBox hurtBox)
        {
            if (!hitBox.IsActive || !hurtBox.IsActive)
                return false;

            Vector2 hitBoxSize = hitBox.Size * 0.5f;
            Vector2 hurtBoxSize = hurtBox.Size * 0.5f;
            Vector2 hitBoxPos = hitBox.Pos;
            Vector2 hurtBoxPos = hurtBox.Pos;

            return hitBoxPos.x + hitBoxSize.x >= hurtBoxPos.x - hurtBoxSize.x &&
                   hitBoxPos.x - hitBoxSize.x <= hurtBoxPos.x + hurtBoxSize.x &&
                   hitBoxPos.y + hitBoxSize.y >= hurtBoxPos.y - hurtBoxSize.y &&
                   hitBoxPos.y - hitBoxSize.y <= hurtBoxPos.y + hurtBoxSize.y;
        }

        public static bool IsHitting(HitBox hitBoxA, HitBox hitBoxB)
        {
            Vector2 hitBoxSize = hitBoxA.Size * 0.5f;
            Vector2 hurtBoxSize = hitBoxB.Size * 0.5f;
            Vector2 hitBoxPos = hitBoxA.Pos;
            Vector2 hurtBoxPos = hitBoxB.Pos;

            return hitBoxPos.x + hitBoxSize.x >= hurtBoxPos.x - hurtBoxSize.x &&
                   hitBoxPos.x - hitBoxSize.x <= hurtBoxPos.x + hurtBoxSize.x &&
                   hitBoxPos.y + hitBoxSize.y >= hurtBoxPos.y - hurtBoxSize.y &&
                   hitBoxPos.y - hitBoxSize.y <= hurtBoxPos.y + hurtBoxSize.y;
        }
    }
}
