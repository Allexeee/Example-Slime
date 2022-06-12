using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  public class JsonType_IObject : JsonConverter<IObject>
  {
    public override void WriteJson(JsonWriter writer, IObject value, JsonSerializer serializer)
    {
      foreach (var item in value)
      {
        var key = $"{item.Key}.Serialize";
        if (App.Converters.ContainsKey(key))
        {
          var converter = App.Converters[key];
          converter(new object[] {writer, item.Value, serializer});
        }
      }
    }

    public override IObject ReadJson(JsonReader reader, Type objectType, IObject existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var jobject = JToken.ReadFrom(reader);

      var result = new DataObject();

      foreach (var jproperty in (JObject) jobject)
      {
        var key       = $"{jproperty.Key}.Deserialize";
        var converter = App.Converters[key];
        converter(new object[] {reader, jproperty.Value, serializer, result});
      }

      return result;
    }
  }
}