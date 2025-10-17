using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 单例模式，不依赖 Unity API
 */
public abstract class Singleton<T> where T : class, new()
{
  private static T _instance;
  private static readonly object _lock = new object();

  public static T Instance
  {
    get
    {
      if (_instance != null) return _instance;
      lock (_lock)
      {
        _instance ??= new T();
      }

      return _instance;
    }
  }
}