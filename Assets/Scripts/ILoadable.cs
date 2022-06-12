using System;
using Newtonsoft.Json;

namespace PixeyeGames.ExampleSlime
{
  public interface ILoadable
  {
    object         Result         { set; }
    JsonSerializer JsonSerializer { get; }
  }
}