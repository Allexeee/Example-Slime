using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixeyeGames.ExampleSlime
{
  static class App
  {
    public static Dictionary<string, Action<object[]>> Converters = new Dictionary<string, Action<object[]>>();

    static App()
    {
      Converters.Add("Transform.Serialize",  Transform.Serialize);
      Converters.Add("Position.Serialize",   Position.Serialize);
      Converters.Add("Position.Deserialize", Position.Deserialize);
    }


    static class Transform
    {
      public static void Serialize(params object[] args)
      {
        var writer     = (JsonWriter) args[0];
        var value      = (UnityEngine.Transform) args[1];
        var serializer = (JsonSerializer) args[2];

        var position = value.position;

        Converters["Position.Serialize"](new object[] {writer, position, serializer});
      }
    }

    static class Position
    {
      public static void Serialize(params object[] args)
      {
        var writer     = (JsonWriter) args[0];
        var value      = (Vector3) args[1];
        var serializer = (JsonSerializer) args[2];

        var jObject = new JObject();

        var jPos = new JObject();
        jPos.Add("X", JToken.FromObject(value.x));
        jPos.Add("Y", JToken.FromObject(value.y));

        jObject.Add("Position", jPos);

        jObject.WriteTo(writer);
      }

      public static void Deserialize(params object[] args)
      {
        var reader     = (JsonReader) args[0];
        var jObject    = (JObject) args[1];
        var serializer = (JsonSerializer) args[2];
        var dObject    = (IObject) args[3];

        var x = jObject["X"].Value<float>();
        var y = jObject["Y"].Value<float>();
        dObject["Position"] = new Vector3(x, y);
      }
    }
  }
}