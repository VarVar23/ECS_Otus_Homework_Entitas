using Entitas;

namespace Gameplay
{
    [Game] public class CountAllyArmy : IComponent { public int Value; }
    [Game] public class CountEnemyArmy : IComponent { public int Value; }
    [Game] public class OffsetXArmy : IComponent { public float Value; }
    [Game] public class OffsetZArmy : IComponent { public float Value; }
    [Game] public class RandomOffsetZArmy : IComponent { public float Value; }
    [Game] public class ArmyCreator : IComponent { }
}