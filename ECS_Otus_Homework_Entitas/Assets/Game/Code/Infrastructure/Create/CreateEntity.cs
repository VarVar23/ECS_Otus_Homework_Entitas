namespace Infrastructure
{
    public static class CreateEntity
    {
        public static GameEntity Game()
        {
            return Contexts.sharedInstance.game.CreateEntity();
        }
    }
}