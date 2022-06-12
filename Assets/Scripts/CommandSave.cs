using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  public class CommandSave : ICommand
  {
    ISavable _obj;

    public const string Path = @"D:\Документы\Example Json\ExampleSlime.json";

    public CommandSave(ISavable obj)
    {
      _obj = obj;
    }

    public void Execute()
    {
      Serialize();
    }

    void Serialize()
    {
      JsonSerializer serializer = JsonSerializer.Create();
      serializer.Formatting = Formatting.Indented;
      serializer.Converters.Add(new JsonType_IObject());

      using (StreamWriter sw = new StreamWriter(Path))
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, _obj.Target);
      }
    }
  }
}