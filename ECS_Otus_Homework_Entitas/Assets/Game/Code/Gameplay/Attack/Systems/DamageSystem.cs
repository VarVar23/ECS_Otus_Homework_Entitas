using Entitas;
using System.Collections.Generic;

namespace Gameplay
{
    public class DamageSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(4);

        public DamageSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hp,
                    GameMatcher.TakeDamage));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                float resultHP = entity.hp.Value - entity.takeDamage.Value;

                if(resultHP > 0)
                {
                    entity.ReplaceHp(resultHP);
                }
                else
                {
                    entity.ReplaceHp(0);
                    entity.isDestroy = true;
                }

                entity.RemoveTakeDamage();
            }
        }
    }
}