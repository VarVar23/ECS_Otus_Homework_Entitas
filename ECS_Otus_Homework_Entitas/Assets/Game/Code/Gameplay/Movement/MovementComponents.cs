using Entitas;
using UnityEngine;

namespace Gameplay.Movement
{
    [Game] public class Speed : IComponent { public float Value; }
    [Game] public class StopDistance : IComponent { public float Value; }
    [Game] public class Direction : IComponent { public Vector3 Value; }
    [Game] public class WorldPosition : IComponent { public Vector3 Value; }
    [Game] public class WorldRotation : IComponent { public Vector3 Value; }
    [Game] public class TransformComponent : IComponent { public Transform Value; }
    [Game] public class Moving : IComponent { }
    [Game] public class MovingForward : IComponent { }
}