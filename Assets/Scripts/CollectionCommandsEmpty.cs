using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PixeyeGames.ExampleSlime
{
  public class CollectionCommandsEmpty : ICollectionCommands
  {
    public IEnumerator<ICommand> GetEnumerator()
    {
      return Enumerable.Empty<ICommand>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void Add(ICommand item)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(ICommand item)
    {
      return false;
    }

    public void CopyTo(ICommand[] array, int arrayIndex)
    {
    }

    public bool Remove(ICommand item)
    {
      return false;
    }

    public int Count => 0;

    public bool IsReadOnly => false;

    public int IndexOf(ICommand item)
    {
      return -1;
    }

    public void Insert(int index, ICommand item)
    {
    }

    public void RemoveAt(int index)
    {
    }

    public ICommand this[int index]
    {
      get => CommandEmpty.Default;
      set { }
    }

    public void Execute()
    {
    }
  }
}