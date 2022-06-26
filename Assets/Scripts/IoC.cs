using System;
using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public static class IoC
  {
    public static IocImpl Main { get; } = IocImpl.CreateDefault();
    public static IArgs   Args => Main.Args;

    public static bool CanGet(Key key)
    {
      return ((IIoc) Main).CanGet(key);
    }

    public static bool TryGet<T>(Key key, IArgs args, out T result)
    {
      var ioc = Main;
      if (!((IIoc) ioc).CanGet(key))
      {
        result = default;
        return false;
      }

      result = Get<T>(key, args);
      return true;
    }

    public static T Get<T>(Key key, IArgs args)
    {
      var ioc      = Main;
      var resolved = ((IIoc) ioc).Get(key)(args);
      if (!(resolved is T result))
        throw new Exception($"(IoC) The returned type does not match the expectation\rExpanded: {typeof(T)} \rResolved: {resolved.GetType()}");

      return result;
    }

    public static T Get<T>(Key key)
    {
      return Get<T>(key, Args.WriteNull());
    }

    public static void Register(Key key, IocResolve resolve)
    {
      Get<ICommand>(new Key("CommandIocRegister"), Args.Write(key, resolve)).Execute();
    }
  }
}