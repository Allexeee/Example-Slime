using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  public interface IAddableToScene
  {
    GameObject Prefab   { get; }
    Vector3    Position { get; }
    Transform  Parent   { get; }
    GameObject Result   { set; }
  }

  public static partial class HelperIoC
  {
    public static IAddableToScene AdapterAddToScene(IObject obj)
    {
      return new AdapterAddToScene(obj);
    }
    
    // public static IAddableToScene Init(this IAddableToScene source, IObject obj, GameObject prefab, Vector3 position)
    // {
    //   obj["Prefab"]   = prefab;
    //   obj["Position"] = position;
    //   obj["Parent"]   = null;
    //   return source;
    // }    
    //
    // public static IAddableToScene Init(this IAddableToScene source, IObject obj, GameObject prefab)
    // {
    //   obj["Prefab"]   = prefab;
    //   // obj["Position"] = default;
    //   obj["Parent"]   = null;
    //   return source;
    // }
    
    public static IAddableToScene Get(this IAddableToScene source, IObject obj, out GameObject result)
    {
      result          = (GameObject) obj["Result"];
      return source;
    }
  }
}