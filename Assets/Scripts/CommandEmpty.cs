namespace PixeyeGames.ExampleSlime
{
  public class CommandEmpty : ICommand
  {
    public static ICommand Default = new CommandEmpty();
    public        void     Execute() { }
  }
}