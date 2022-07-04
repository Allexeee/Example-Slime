using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using ExampleData;
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
    public static DToken CreateExample()
    {
      var result = new DObject();
      result["Properties"]["Position"].OnChanged += PositionOnChanged;

      result["Properties"]["Position"] = DConverter.To(new Vector2(5f, 0f));

      RegisterObject(result);
      return result;
    }

    static void PositionOnChanged(object sender, EventArgs e)
    {
      var dObject    = sender as DObject;
      var gameObject = GetGameObject(dObject);

      var newValue = GetPosition(dObject);

      gameObject.transform.position = newValue;
    }

    static Vector2 GetPosition(DObject dObject)
    {
      var dValue = dObject["Position"].Value;
      var value  = DConverter.From<Vector2>(dValue);

      return value;
    }

    static GameObject GetGameObject(DObject dObject)
    {
      var dProperty = dObject.Parent["Cache"]["GameObject"] as DProperty;
      var value     = dProperty.Value;

      GameObject gameObject;

      if (value is DObject)
      {
        gameObject      = CreateGameObject();
        dProperty.Value = DValue.CreateFrom(gameObject);
      }
      else
      {
        gameObject = ((DValue<GameObject>) value).Val;
      }

      return gameObject;
    }

    static GameObject CreateGameObject()
    {
      return new GameObject();
    }

    static void RegisterObject(DObject obj)
    {
      IoC.Get<IList<DObject>>("Objects").Add(obj);
    }
  }
}