using UnityEngine;
using System;

namespace Jump
{
    [Serializable]
    public class GroundingChecker : IGroundingChecker
    {
        [SerializeField, Range(0f, 5f)]
        private float _groundRayDistance;
        [SerializeField] 
        private Vector3 _groundRayPosition;
        [SerializeField] 
        private LayerMask _groundLayer;

        [SerializeField] 
        private Color _rayColor;
        
        public float GroundRayDistance { get => _groundRayDistance; set => _groundRayDistance = value; }
        public Vector3 GroundRayPosition { get => _groundRayPosition; set => _groundRayPosition = value; }
        public LayerMask GroundLayer { get => _groundLayer; set => _groundLayer = value; }
        public Color RayColor { get => _rayColor; set => _rayColor = value; }
        public bool GroundingByRaycast(Vector3 objectPosition) => Physics2D.Raycast(objectPosition + GroundRayPosition, Vector2.down,
            GroundRayDistance, GroundLayer);
    }
}