namespace Circus.App.Entities
{
    public class Monkey(string name)
    {
        public string Name { get { return name; } }

        public void ExecuteTrick(Trick trick)
        {
            Console.WriteLine($"{name} executed trick \"{trick.Name}\"");
        }
    }
}
