using UnityEngine;

namespace PixeyeGames.ExampleSlime
{
  class AdapterAddToScene : IAddableToScene
  {
    IObject _obj;

    public AdapterAddToScene(IObject obj)
    {
      _obj = obj;
    }

    public GameObject Prefab => (GameObject) _obj[nameof(Prefab)];

    public Vector3 Position => (Vector3) _obj[nameof(Position)];

    public Transform Parent => (Transform) _obj[nameof(Parent)];

    public GameObject Result
    {
      set => _obj[nameof(Result)] = value;
    }
  }
}