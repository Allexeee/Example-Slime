using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExampleData
{
  public class DObject : DToken
  {
    List<DProperty> Properties = new List<DProperty>();

    public override DObject Parent { get; set; }

    public override DToken Value
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public override DToken this[string propertyName]
    {
      get
      {
        var property = Properties.FirstOrDefault(src => src.Name == propertyName);
        if (property == default)
        {
          property = new DProperty()
          {
            Name = propertyName,
            Value = new DObject()
            {
              Parent = this,
            }
          };
          Properties.Add(property);
        }

        return property;
      }
      set
      {
        if (value is DProperty dProperty)
          Properties.Add(dProperty);
        else if (value is DObject dObject)
        {
          var property       = (DProperty) this[propertyName];
          var dPropertyValue = (DObject) property.Value;
          for (var index = 0; index < dObject.Properties.Count; index++)
          {
            var objectProperty = dObject.Properties[index];
            dPropertyValue.Properties.Add(objectProperty);
          }
        }
        else if (value is DToken dToken)
        {
          Properties.Add(new DProperty()
          {
            Name  = propertyName,
            Value = dToken
          });
        }
        else
        {
          throw new Exception();
        }

        // Parent?.OnChanged?.Invoke(this, EventArgs.Empty);
        OnChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    public override event EventHandler OnChanged;
  }

  public class DProperty : DToken
  {
    public override DObject Parent
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public override DToken this[string propertyName]
    {
      get
      {
        if (Value is DObject dObject)
          return dObject[propertyName];

        throw new Exception();
      }
      set
      {
        if (value is DObject dObject)
        {
          Value[propertyName] = dObject;
        }
        else
          throw new Exception();
      }
    }

    public override event EventHandler OnChanged
    {
      add
      {
        if (Value.Parent != null)
          Value.Parent.OnChanged += value;
      }
      remove
      {
        if (Value.Parent != null)
          Value.Parent.OnChanged -= value;
      }
    }

    public string Name  { get; set; }
    public override DToken Value { get; set; }
  }

  public class DValue : DToken
  {
    public override DObject Parent
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public override DToken Value { get; set; }

    public override DToken this[string propertyName]
    {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override event EventHandler OnChanged;

    public static DToken CreateFrom<T>(T obj)
    {
      var dValue = new DValue<T>()
      {
        Val = obj
      };
      dValue.Value = dValue;
      return dValue;
    }
  }

  public class DValue<T> : DValue
  {
    public override DObject Parent
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public override DToken this[string propertyName]
    {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override event EventHandler OnChanged;
    
    public T Val { get; set; }
  }

  public abstract class DToken
  {
    public abstract DObject Parent { get; set; }

    public abstract DToken Value { get; set; }

    public abstract DToken this[string propertyName] { get; set; }

    public abstract event EventHandler OnChanged;
  }

  public class Converters : Dictionary<Type, IConvertable>
  {
  }

  public interface IConvertable
  {
    DToken To(object   obj);
    object From(DToken obj);
  }

  public abstract class Converter<T> : IConvertable
  {
    protected abstract DToken To(T        obj);
    protected abstract T      From(DToken obj);

    DToken IConvertable.To(object obj)
    {
      return To((T) obj);
    }

    object IConvertable.From(DToken obj)
    {
      return From(obj);
    }
  }

  class Converter_Vector2 : Converter<Vector2>
  {
    protected override DToken To(Vector2 obj)
    {
      var dObject = new DObject();
      dObject["X"] = DConverter.To(obj.x);
      dObject["Y"] = DConverter.To(obj.y);
      return dObject;
    }

    protected override Vector2 From(DToken obj)
    {
      var dObject = (DObject) obj;
      var result  = default(Vector2);
      result.x = DConverter.From<float>(dObject["X"].Value);
      result.y = DConverter.From<float>(dObject["Y"].Value);
      return result;
    }
  }

  class Converter_Float : Converter<float>
  {
    protected override DToken To(float obj)
    {
      return DValue.CreateFrom(obj);
    }

    protected override float From(DToken obj)
    {
      var dValue = (DValue<float>) obj;
      return dValue.Val;
    }
  }

  static class DConverter
  {
    static Converters Converters { get; } = new Converters()
    {
      {typeof(float), new Converter_Float()},
      {typeof(Vector2), new Converter_Vector2()},
    };

    public static DToken To(object obj)
    {
      if (Converters.TryGetValue(obj.GetType(), out var converter))
        return converter.To(obj);

      throw new Exception();
    }

    public static T From<T>(DToken obj)
    {
      if (Converters.TryGetValue(typeof(T), out var converter))
        return (T) converter.From(obj);

      throw new Exception();
    }
  }

  public class Example
  {
    public void Create()
    {
      var dobject = new DObject();
      var con     = DConverter.To(new Vector2(5f, 1f));
      dobject["Properties"].OnChanged             += OnPropertiesChanged;
      dobject["Properties"]["Position"].OnChanged += OnPositionChanged;
      dobject["Properties"]["Position"]           =  con;


      var i = 0;
      Debug.Log($"Example.Created: {dobject}");
    }

    static void OnPositionChanged(object sender, EventArgs e)
    {
      Debug.Log($"OnPositionChanged\r{sender}\r{e}");
    }

    static void OnPropertiesChanged(object sender, EventArgs e)
    {
      Debug.Log($"OnPropertiesChanged\r{sender}\r{e}");
    }
  }
}