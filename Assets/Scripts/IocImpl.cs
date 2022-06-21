using System.Collections.Generic;
using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public class IocImpl : IocImpl<Key>
  {
    IocImpl(Dictionary<Key, object> storage, IArgs args) : base(storage, args)
    {
    }

    public static IocImpl CreateDefault()
    {
      var storage = new Dictionary<Key, object>();
      var args    = new RewritableArgs(8);
      var result  = new IocImpl(storage, args);

      new CommandSetToDictionary(storage, "CommandIocRegister", (IocResolve) FuncCommandRegister).Execute();
      return result;


      ICommand FuncCommandRegister(IArgs _args)
      {
        var key   = (Key) _args[0];
        var value = (IocResolve) _args[1];
        return new CommandSetToDictionary(storage, key, value);
      }
    }
  }
}