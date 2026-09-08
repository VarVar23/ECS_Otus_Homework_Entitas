using Entitas;
using Infrastructure;

namespace Gameplay
{
    public class AttackCooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _weapons;

        public AttackCooldownSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;

            _weapons = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cooldown,
                    GameMatcher.CooldownLeft,
                    GameMatcher.StartShoot));
        }

        public void Execute()
        {
            foreach (var weapon in _weapons)
            {
                weapon.ReplaceCooldownLeft(weapon.cooldownLeft.Value - _timeService.DeltaTime);

                if(weapon.cooldownLeft.Value <= 0)
                {
                    weapon.ReplaceCooldownLeft(weapon.cooldown.Value);
                    weapon.isShoot = true;
                }
            }
        }
    }
}