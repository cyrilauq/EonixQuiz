using Circus.App.Entities;

var spectator = new Spectator();
var monkey1 = new Monkey("singe 1");
var monkey2 = new Monkey("singe 2");
var dressor1 = new Dressor(monkey1);
var dressor2 = new Dressor(monkey2);
Trick[] tricks1 = [
    new Trick("Turn around")
];
Trick[] tricks2 = [
    new Trick("Turn around")
];

for (int i = 0; i < tricks1.Length && i < tricks2.Length; i++)
{
    Console.WriteLine(dressor1.MakeDoTrick(tricks1[i]));
    Console.WriteLine(spectator.Applause(tricks1[i], monkey1.Name));
    Console.WriteLine(monkey2.ExecuteTrick(tricks2[i]));
    Console.WriteLine(spectator.Applause(tricks2[i], monkey2.Name));
}
