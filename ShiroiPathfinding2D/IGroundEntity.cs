using UnityEngine;

namespace Shiroi.Pathfinding2D {
    public interface IGroundEntity {
        void Move(Vector2 input);
        float GetMaxJumpHeight();
    }
}