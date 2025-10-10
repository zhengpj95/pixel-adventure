using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/**
 * 类型安全的字符串枚举基类
 * 支持字符串 《===》 枚举对象互转
 * 支持 FromName / TryParse / Values 等工具方法
 * 自动缓存反射结果，性能友好
 */
public abstract class StringEnum<T> where T : StringEnum<T>
{
  public string Name { get; private set; }

  protected StringEnum(string name)
  {
    Name = name;
  }

  public override string ToString() => Name;

  // ✅ 相等比较
  public override bool Equals(object obj)
  {
    return obj is StringEnum<T> other && Name == other.Name;
  }

  public override int GetHashCode() => Name.GetHashCode();

  public static bool operator ==(StringEnum<T> a, StringEnum<T> b)
  {
    if (ReferenceEquals(a, b)) return true;
    if (a is null || b is null) return false;
    return a.Name == b.Name;
  }

  public static bool operator !=(StringEnum<T> a, StringEnum<T> b) => !(a == b);

  // ✅ 缓存所有定义的值（类似 Enum.GetValues）
  private static List<T> _values;

  public static IReadOnlyList<T> Values
  {
    get
    {
      if (_values == null)
      {
        _values = typeof(T)
          .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
          .Where(f => f.FieldType == typeof(T))
          .Select(f => (T)f.GetValue(null))
          .ToList();
      }

      return _values;
    }
  }

  // ✅ 根据字符串值查找（找不到返回 null）
  public static T FromName(string name)
  {
    if (string.IsNullOrEmpty(name)) return null;
    return Values.FirstOrDefault(v => v.Name == name);
  }

  // ✅ 安全解析（不会抛异常）
  public static bool TryParse(string name, out T result)
  {
    result = FromName(name);
    return result != null;
  }

  // ✅ 隐式转换为 string
  public static implicit operator string(StringEnum<T> value)
  {
    return value?.Name;
  }

  // ✅ 隐式转换为 枚举对象
  public static implicit operator StringEnum<T>(string name)
  {
    return FromName(name);
  }
}

// 案例
// public sealed class UIEvent : StringEnum<UIEvent>
// {
//   public static readonly UIEvent OpenPanel = new("OpenPanel");
//   public static readonly UIEvent ClosePanel = new("ClosePanel");
//
//   private UIEvent(string name) : base(name) { }
// }