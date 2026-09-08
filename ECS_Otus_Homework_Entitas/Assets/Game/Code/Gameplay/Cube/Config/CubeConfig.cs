using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Config/CubeConfig", fileName = "CubeConfig")]
    public class CubeConfig : ScriptableObject
    {
        [field: SerializeField] public float MinCooldown { get; private set; }
        [field: SerializeField] public float MaxCooldown { get; private set; }
        [field: SerializeField] public float Hp { get; private set; }
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MinStopDistance { get; private set; }
        [field: SerializeField] public float MaxStopDistance { get; private set; }
    }
}