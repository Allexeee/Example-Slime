using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExampleData
{
  public class DObject : DToken
  {
    List<DProperty> Properties = new List<DProperty>();

    public override DToken this[string propertyName]
    {
      get
      {
        var property = Properties.FirstOrDefault(src => src.Name == propertyName);
        if (property == default)
        {
          property           = new DProperty();
          this[propertyName] = property;
        }

        return property;
      }
      set
      {
        if (value is DProperty dProperty)
          Properties.Add(dProperty);
        throw new Exception();
      }
    }

    public override event EventHandler OnChanged;
  }

  public class DProperty : DToken
  {
    public override DToken this[string propertyName]
    {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override event EventHandler OnChanged;

    public string Name
    {
      get { throw new NotImplementedException(); }
    }

    public DToken Value
    {
      get { throw new NotImplementedException(); }
    }
  }

  public class DValue : DToken
  {
    public override DToken this[string propertyName]
    {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override event EventHandler OnChanged;

    public static DToken CreateFrom<T>(T obj)
    {
      return new DValue<T>()
      {
        Value = obj
      };
    }
  }

  public class DValue<T> : DToken
  {
    public override DToken this[string propertyName]
    {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override event EventHandler OnChanged;
    public T                           Value { get; set; }
  }

  public abstract class DToken
  {
    public abstract DToken this[string propertyName] { get; set; }

    public abstract event EventHandler OnChanged;
  }

  public class Example
  {
    public void Create()
    {
      var dobject = new DObject();
      dobject["Properties"]["Position"].OnChanged += OnPositionChanged;
      dobject["Properties"]["Position"]           =  DValue.CreateFrom(new Vector2(5f, 1f));
    }

    static void OnPositionChanged(object sender, EventArgs e)
    {
      Debug.Log($"OnPositionChanged\r{sender}\r{e}");
    }
  }
}