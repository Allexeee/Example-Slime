using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PixeyeGames.Ioc;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace PixeyeGames.ExampleSlime
{
  public static class Factory
  {
    public static DataExampleObject CreateExample()
    {
      var result = new DataExampleObject();
      // result.PropertyChanged += OnPositionChanged;

      ((JObject) result["Properties"]).PropertyChanged             += OnPropertiesChanged;
      ((JObject) result["Properties"]["Position"]).PropertyChanged += OnPositionChanged;
      // ((JObject) result["Properties"]["Position"]["X"]).PropertyChanged += OnPositionChanged;
      // ((JObject) result["Properties"]["Position"]["Y"]).PropertyChanged += OnPositionChanged;

      result["Properties"]["Position"] = JToken.FromObject(new Vector3(5f, 2f, 0f));
      RegisterObject(result);
      return result;
    }

    static void OnPropertiesChanged(object sender, PropertyChangedEventArgs e)
    {
      Debug.Log($"OnPropertiesChanged\r{sender}\r{e.PropertyName}");
    }

    static void OnPositionChanged(object sender, PropertyChangedEventArgs e)
    {
      Debug.Log($"OnPositionChanged\r{sender}\r{e.PropertyName}");
    }

    static void RegisterObject(DataExampleObject obj)
    {
      IoC.Get<IList<DataExampleObject>>("Objects").Add(obj);
    }
  }
}
