using UnityEngine;

namespace Infrastructure
{
    public class PhysicsService : IPhysicsService
    {
        public bool RayCast(Vector3 origin, Vector3 direction, float distance, int layerMask)
        {
            return Physics.Raycast(origin, direction, distance, layerMask);
        }
    }
}