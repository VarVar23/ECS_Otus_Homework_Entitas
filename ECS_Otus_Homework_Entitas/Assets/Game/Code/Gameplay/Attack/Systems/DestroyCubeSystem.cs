using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DestroyCubeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cubes;
        private readonly IGroup<GameEntity> _allWithTarget;
        private readonly List<GameEntity> _buffer = new(2);
        private readonly List<GameEntity> _buffer2 = new(2);

        public DestroyCubeSystem(GameContext context)
        {
            _cubes = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destroy,
                    GameMatcher.Cube,
                    GameMatcher.Transform));

            _allWithTarget = context.GetGroup(GameMatcher.
                AllOf(
                    GameMatcher.Target));
        }

        public void Execute()
        {
            foreach (var cube in _cubes.GetEntities(_buffer))
            {
                foreach(var withTarget in _allWithTarget.GetEntities(_buffer2))
                {
                    if(withTarget.target.Value == cube)
                    {
                        withTarget.RemoveTarget();
                        withTarget.isFindTarget = true;
                    }
                }

                GameObject.Destroy(cube.transform.Value.gameObject);
                cube.Destroy();
            }
        }
    }
}