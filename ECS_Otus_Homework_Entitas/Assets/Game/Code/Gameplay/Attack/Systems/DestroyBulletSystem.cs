using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DestroyBulletSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _bullets;
        private readonly List<GameEntity> _buffer = new(2);

        public DestroyBulletSystem(GameContext context)
        {
            _bullets = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destroy,
                    GameMatcher.Bullet,
                    GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (var bullet in _bullets.GetEntities(_buffer))
            {
                GameObject.Destroy(bullet.transform.Value.gameObject);
                bullet.Destroy();
            }
        }
    }
}