using System.Collections.Generic;

namespace PixeyeGames.ExampleSlime
{
  public class CollectionCommands : List<ICommand>, ICollectionCommands
  {
    public static ICollectionCommands Empty = new CollectionCommandsEmpty();

    public void Execute()
    {
      foreach (var command in this)
        command.Execute();
    }
  }
}