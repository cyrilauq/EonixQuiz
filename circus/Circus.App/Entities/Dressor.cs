namespace Circus.App.Entities
{
    public class Dressor(Monkey monkey)
    {
        public void MakeDoTrick(Trick trick)
        {
            monkey.ExecuteTrick(trick);
        }
    }
}
