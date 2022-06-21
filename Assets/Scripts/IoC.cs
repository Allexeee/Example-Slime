using PixeyeGames.Ioc;

namespace PixeyeGames.ExampleSlime
{
  public static class IoC
  {
    public static IocImpl Main { get; } = IocImpl.CreateDefault();
    public static IArgs   Args => Main.Args;
  }
}