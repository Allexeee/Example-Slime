using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  public class CommandRemoveFromScene : ICommand
  {
    IRemovableFromScene _obj;

    public CommandRemoveFromScene(IRemovableFromScene obj)
    {
      _obj = obj;
    }

    public void Execute()
    {
      Object.Destroy(_obj.GameObject);
    }
  }
}