using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PixeyeGames.ExampleSlime
{
  public class CommandAction : ICommand
  {
    Action _action;

    public CommandAction(Action action)
    {
      _action = action;
    }

    public void Execute()
    {
      _action();
    }
  }
}