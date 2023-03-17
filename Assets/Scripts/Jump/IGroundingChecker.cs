using UnityEngine;

namespace Jump
{
    public interface IGroundingChecker
    {
        float GroundRayDistance { get; set; }
        Vector3 GroundRayPosition { get; set; }
        LayerMask GroundLayer { get; set; }
        Color RayColor { get; set; }

        bool GroundingByRaycast(Vector3 objectPosition);
    }
}