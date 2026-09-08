using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Config/CubeArmy", fileName = "CubeArmy")]
    public class CubeArmyConfig : ScriptableObject
    {
        [field: SerializeField] public int CountAllyCubes { get; private set; }
        [field: SerializeField] public int CountEnemiesCubes { get; private set; }
        [field: SerializeField] public float OffsetX { get; private set; }
        [field: SerializeField] public float OffsetZ { get; private set; }
        [field: SerializeField] public float RandomOffsetZ { get; private set; }
    }
}