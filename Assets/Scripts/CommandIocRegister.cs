using System;
using System.Collections.Generic;
using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public class CommandIocRegister : ICommand
  {
    Dictionary<Key, object> _storage;
    IocResolve              _value;
    Key                     _key;

    public CommandIocRegister(Dictionary<Key, object> storage, Key key, IocResolve value)
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