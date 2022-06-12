using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PixeyeGames.ExampleSlime
{
  class AdapterLoadJson : ILoadable
  {
    IObject _obj;

    public AdapterLoadJson(IObject obj)
    {
      _obj = obj;
    }

    public object Result
    {
      set => _obj["Result"] = value;
    }

    public JsonSerializer JsonSerializer { get; } = CreateSer();

    static JsonSerializer CreateSer()
    {
      JsonSerializer serializer = JsonSerializer.Create();
      serializer.Formatting = Formatting.Indented;
      serializer.Converters.Add(new JsonType_IObject());
      return serializer;
    }
  }

  public static partial class HelperIoC
  {
    public static ILoadable AdapterLoad(IObject obj)
    {
      return new AdapterLoadJson(obj);
    }

    public static ILoadable Get<T>(this ILoadable source, IObject data, out T result)
    {
      var jtoken   = (JToken) data["Result"];
      var settings = source.JsonSerializer;
      result = jtoken.ToObject<T>(settings);
      return source;
    }
  }
}