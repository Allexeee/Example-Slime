using System.Collections.Generic;
using System.Collections.ObjectModel;
using PixeyeGames.Ioc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixeyeGames.ExampleSlime
{
  class MonoExample : MonoBehaviour
  {
    [SerializeField]
    List<DataExampleObject> Collection = new List<DataExampleObject>();

    void Awake()
    {
      IoC.Register("Objects", args => Collection);
      // IoC.Register("Properties.Position.Set", args =>
      // {
      //   var data  = (DataExampleObject) args[0];
      //   var value = args[1];
      //
      //   if (!data["Properties"]["Position"].HasValues)
      //     return new CommandAction(() =>
      //     {
      //       data["Properties"]["Position"]["X"] = 0f;
      //       data["Properties"]["Position"]["Y"] = 0f;
      //     });
      //
      //   return CommandEmpty.Default;
      // });
    }

    void Update()
    {
      if (Input.GetKeyUp(KeyCode.Alpha1))
      {
        Factory.CreateExample();
      }
    }
  }
}