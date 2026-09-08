using UnityEngine;

namespace Infrastructure
{
    public interface IPhysicsService
    {
        bool RayCast(Vector3 origin, Vector3 direction, float distance, int layerMask);
    }
}