using System;
using System.Collections.Generic;
using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public class CommandSetToDictionary : ICommand
  {
    Dictionary<Key, object> _storage;
    object                  _value;
    Key                     _key;

    public CommandSetToDictionary(Dictionary<Key, object> storage, Key key, object value)
    {
      _storage = storage;
      _value   = value;
      _key     = key;
    }

    public void Execute()
    {
      _storage[_key] = _value;
    }
  }
}