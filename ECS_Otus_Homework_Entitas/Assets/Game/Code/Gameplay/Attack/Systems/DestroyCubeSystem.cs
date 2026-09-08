using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DestroyCubeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cubes;
        private readonly List<GameEntity> _buffer = new(2);

        public DestroyCubeSystem(GameContext context)
        {
            _cubes = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destroy,
                    GameMatcher.Cube,
                    GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (var cube in _cubes.GetEntities(_buffer))
            {
                GameObject.Destroy(cube.transform.Value.gameObject);
                cube.Destroy();
            }
        }
    }
}