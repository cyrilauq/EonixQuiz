namespace Circus.App.Entities
{
    public class Monkey(string name)
    {
        public string Name { get { return name; } }

        public string ExecuteTrick(Trick trick)
        {
            return $"{name} executed trick \"{trick.Name}\"";
        }
    }
}
