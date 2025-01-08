namespace Circus.App.Entities
{
    public class Spectator
    {
        public void Applause(Trick trick, string animalName)
        {
            Console.WriteLine($"spectateur applaudit pendant le tour d'acrobatie '{trick.Name}' du {animalName}");
        }
    }
}
