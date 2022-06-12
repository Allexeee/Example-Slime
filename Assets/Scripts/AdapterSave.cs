namespace PixeyeGames.ExampleSlime
{
  class AdapterSave : ISavable
  {
    IObject _obj;

    public AdapterSave(IObject obj)
    {
      _obj = obj;
    }

    public object Target => _obj["Target"];
  }

  public static partial class HelperIoC
  {
    public static ISavable AdapterSave(IObject obj)
    {
      return new AdapterSave(obj);
    }

    public static ISavable Init(this ISavable source, IObject obj, object serializable)
    {
      obj["Target"] = serializable;
      return source;
    }
  }
}