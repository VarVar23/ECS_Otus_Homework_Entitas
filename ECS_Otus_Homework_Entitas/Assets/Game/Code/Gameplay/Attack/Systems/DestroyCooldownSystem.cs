using Entitas;
using Infrastructure;

namespace Gameplay
{
    public class DestroyCooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _bullets;

        public DestroyCooldownSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;

            _bullets = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cooldown,
                    GameMatcher.CooldownLeft,
                    GameMatcher.Bullet));
        }

        public void Execute()
        {
            foreach (var bullet in _bullets)
            {
                bullet.ReplaceCooldownLeft(bullet.cooldownLeft.Value - _timeService.DeltaTime);

                if (bullet.cooldownLeft.Value <= 0)
                {
                    bullet.isDestroy = true;
                }
            }
        }
    }
}