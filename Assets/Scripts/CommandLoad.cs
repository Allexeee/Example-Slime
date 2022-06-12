using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  public class CommandLoad : ICommand
  {
    ILoadable _obj;

    string Path => CommandSave.Path;

    public CommandLoad(ILoadable obj)
    {
      _obj = obj;
    }

    public void Execute()
    {
      Deserialize();
    }

    void Deserialize()
    {
      JsonSerializer serializer = _obj.JsonSerializer;

      using (StreamReader sr = new StreamReader(Path))
      using (JsonReader reader = new JsonTextReader(sr))
      {
        _obj.Result = serializer.Deserialize(reader);
      }
    }
  }
}