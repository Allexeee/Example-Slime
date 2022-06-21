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
    GameObject _prefab;

    IList<IObject> Collection { get; } = new Collection<IObject>();

    void Awake()
    {
      IoC.Main.Register("Example", Callback);
    }

    object Callback(IArgs args)
    {
      Debug.Log($"Example");
      return _prefab;
    }

    void Update()
    {
      if (Input.GetKeyUp(KeyCode.Alpha1))
      {
        var dCreate = new DataObject();
        WriteConfigDataObject(dCreate);
        WriteDataObject(dCreate, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
        CreateGameObject(dCreate);
      }

      if (Input.GetKeyUp(KeyCode.Alpha2))
      {
        Save();
      }

      if (Input.GetKeyUp(KeyCode.Alpha3))
      {
        Load();
      }

      if (Input.GetKeyUp(KeyCode.Alpha4))
      {
        IoC.Main.Get<object>("Example");
      }
    }

    void WriteConfigDataObject(IObject dCreate)
    {
      dCreate["Prefab"] = _prefab;
      dCreate["Parent"] = null;
    }

    void WriteDataObject(IObject dCreate, Vector3 position)
    {
      dCreate["Position"] = position;
    }

    void CreateGameObject(IObject dCreate)
    {
      var adapter   = HelperIoC.AdapterAddToScene(dCreate);
      var cmdCreate = new CommandAddToScene(adapter);
      cmdCreate.Execute();

      adapter.Get(dCreate, out var go);
      go.SetActive(true);

      var dObject = new DataObject();
      dObject["Transform"] = go.transform;

      Collection.Add(dObject);
    }

    void Save()
    {
      var data    = new DataObject();
      var adapter = HelperIoC.AdapterSave(data).Init(data, Collection);
      var cmdSave = new CommandSave(adapter);

      cmdSave.Execute();
    }

    void Load()
    {
      var data    = new DataObject();
      var adapter = HelperIoC.AdapterLoad(data);
      var cmdLoad = new CommandLoad(adapter);

      cmdLoad.Execute();
      adapter.Get(data, out IList<IObject> collection);

      foreach (var item in collection)
      {
        WriteConfigDataObject(item);
        CreateGameObject(item);
      }
    }
  }
}