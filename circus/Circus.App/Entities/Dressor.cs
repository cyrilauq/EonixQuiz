namespace Circus.App.Entities
{
    public class Dressor(Monkey monkey)
    {
        public string MakeDoTrick(Trick trick)
        {
            return monkey.ExecuteTrick(trick);
        }
    }
}
