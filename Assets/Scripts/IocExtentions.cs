using System;
using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public static class IocExtentions
  {
    public static bool CanGet(this IIoc ioc, Key key)
    {
      return ioc.CanGet(key);
    }

    public static bool TryGet<T>(this IIoc ioc, Key key, IArgs args, out T result)
    {
      if (!ioc.CanGet(key))
      {
        result = default;
        return false;
      }

      result = Get<T>(ioc, key, args);
      return true;
    }

    public static T Get<T>(this IIoc ioc, Key key, IArgs args)
    {
      var resolved = ioc.Get(key)(args);
      if (!(resolved is T result))
        throw new Exception($"(IoC) The returned type does not match the expectation\rExpanded: {typeof(T)} \rResolved: {resolved.GetType()}");

      return result;
    }
    
    public static T Get<T>(this IocImpl ioc, Key key)
    {
      return Get<T>(ioc, key, ioc.Args.WriteNull());
    }

    public static void Register(this IocImpl ioc, Key key, IocResolve resolve)
    {
      ioc.Get<ICommand>(new Key("CommandIocRegister"), ioc.Args.Write(key, resolve)).Execute();
    }
  }
}