using Infrastructure;

namespace Gameplay
{
    public class FinishFactory : IFinishFactory
    {
        public GameEntity Create()
        {
            var entity = CreateEntity.Game();
            entity.isFinish = true;
  
            return entity;
        }
    }
}