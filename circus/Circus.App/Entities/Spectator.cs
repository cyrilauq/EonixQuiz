namespace Circus.App.Entities
{
    public class Spectator
    {
        public string Applause(Trick trick, string animalName)
        {
            return $"spectateur applaudit pendant le tour d'acrobatie '{trick.Name}' du {animalName}";
        }
    }
}
