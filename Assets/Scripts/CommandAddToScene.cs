using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  public class CommandAddToScene : ICommand
  {
    IAddableToScene _obj;

    public CommandAddToScene(IAddableToScene obj)
    {
      _obj = obj;
    }

    public void Execute()
    {
      var result = Object.Instantiate(_obj.Prefab, _obj.Position, Quaternion.identity, _obj.Parent);
      _obj.Result = result;
    }
  }
}