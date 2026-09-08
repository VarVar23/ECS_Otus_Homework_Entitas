using Infrastructure;

namespace Gameplay
{
    public class AttackFeature : Feature
    {
        public AttackFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<AttackCooldownSystem>());
            Add(systemFactory.Create<SpawnBulletSystem>());
            Add(systemFactory.Create<DestroyCooldownSystem>());
            Add(systemFactory.Create<DestroyBulletSystem>());
            Add(systemFactory.Create<DestroyCubeSystem>());
            Add(systemFactory.Create<StopShootSystem>());
        }
    }
}