using System;
using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public static class IocArgsExtention
  {
    public static IArgs WriteNull(this IArgs args)
    {
      args.Reset();
      return args;
    }
    
    public static IArgs Write(this IArgs args, Action arg0)
    {
      args.Reset();
      args.Add(arg0);
      return args;
    }

    public static IArgs Write(this IArgs args, Key arg0, IocResolve arg1)
    {
      args.Reset();
      args.Add(arg0).Add(arg1);
      return args;
    }

    public static IArgs Write(this IArgs args, object arg0)
    {
      args.Reset();
      args.Add(arg0);
      return args;
    }

    public static IArgs Write(this IArgs args, object arg0, object arg1)
    {
      args.Reset();
      args.Add(arg0).Add(arg1);
      return args;
    }

    public static IArgs Write(this IArgs args, object arg0, object arg1, object arg2)
    {
      args.Reset();
      args.Add(arg0).Add(arg1).Add(arg2);
      return args;
    }

    public static IArgs Write(this IArgs args, object arg0, object arg1, object arg2, object arg3)
    {
      args.Reset();
      args.Add(arg0).Add(arg1).Add(arg2).Add(arg3);
      return args;
    }
  }
}