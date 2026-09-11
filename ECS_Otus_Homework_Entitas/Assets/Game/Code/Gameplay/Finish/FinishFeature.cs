using Infrastructure;

namespace Gameplay
{
    public class FinishFeature : Feature
    {
        public FinishFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<FinishSystem>());
        }
    }
}