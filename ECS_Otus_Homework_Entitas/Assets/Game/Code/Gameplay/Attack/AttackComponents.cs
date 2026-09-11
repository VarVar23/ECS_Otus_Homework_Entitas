using Entitas;

namespace Gameplay
{
    [Game] public class Damage : IComponent { public float Value; }
    [Game] public class TakeDamage : IComponent { public float Value; }
    [Game] public class Cooldown : IComponent { public float Value; }
    [Game] public class CooldownLeft : IComponent { public float Value; }
    [Game] public class StartShoot : IComponent { }
    [Game] public class Destroy : IComponent { }
    [Game] public class Shoot : IComponent { }
    [Game] public class Bullet : IComponent { }
}