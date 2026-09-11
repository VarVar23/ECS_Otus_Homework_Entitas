using Entitas;
using Infrastructure;

namespace Gameplay
{
    [Game] public class ViewComponent : IComponent { public EntityMonoView Value; }
    [Game] public class Target : IComponent { public GameEntity Value; }
    [Game] public class IdComponent : IComponent { public int Value; }
    [Game] public class FindTarget : IComponent { }
    [Game] public class Cube : IComponent { };
    [Game] public class Enemy : IComponent { };
    [Game] public class Ally : IComponent { };
}