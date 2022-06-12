using System.Collections;
using System.Collections.Generic;

namespace PixeyeGames.ExampleSlime
{
  public interface IObject : IEnumerable<KeyValuePair<string, object>>
  {
    object this[string   key] { get; set; }
    bool Contains(string key);
  }

  class DataObject : IObject
  {
    Dictionary<string, object> _dictionary = new Dictionary<string, object>();

    public object this[string key]
    {
      get => _dictionary[key];
      set => _dictionary[key] = value;
    }

    public bool Contains(string key)
    {
      return _dictionary.ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}