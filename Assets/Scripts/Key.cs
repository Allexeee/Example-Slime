using System;

namespace PixeyeGames.ExampleSlime
{
  public class Key : IEquatable<Key>
  {
    readonly string _key;
    readonly int    _hash;

    public Key(string key)
    {
      _key  = key;
      _hash = _key.GetHashCode();
    }

    public bool Equals(Key other)
    {
      return _key == other._key;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (!obj.GetType().IsAssignableFrom(typeof(Key))) return false;
      return Equals((Key) obj);
    }

    public override int GetHashCode()
    {
      return _hash;
    }

    public override string ToString()
    {
      return _key;
    }

    public static implicit operator Key(string key)
    {
      return new Key(key);
    }

    public static implicit operator string(Key key)
    {
      return key.ToString();
    }

    public static bool operator ==(Key key0, Key key1)
    {
      return !(key0 is null) && key0.Equals(key1);
    }

    public static bool operator !=(Key key0, Key key1)
    {
      return !(key0 == key1);
    }
  }
}