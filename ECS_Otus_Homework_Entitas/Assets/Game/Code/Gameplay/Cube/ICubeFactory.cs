using UnityEngine;

namespace Gameplay
{
    public interface ICubeFactory
    {
        GameEntity CreateAlly(Vector3 position, Vector3 rotation);
        GameEntity CreateEnemy(Vector3 position, Vector3 rotation);
    }
}