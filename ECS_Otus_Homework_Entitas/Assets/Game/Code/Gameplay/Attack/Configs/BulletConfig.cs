using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Config/BulletConfig", fileName = "BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Hp { get; private set; }
        [field: SerializeField] public float TimeLife { get; private set; }
    }
}