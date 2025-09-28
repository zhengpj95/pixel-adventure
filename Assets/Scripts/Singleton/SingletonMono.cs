using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 单例模式，依赖 Unity API，需挂载到unity对象上
 */
public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T _instance;

  public static T Instance
  {
    get
    {
      if (_instance != null) return _instance;
      _instance = FindObjectOfType<T>();
      if (_instance != null) return _instance;
      var obj = new GameObject(typeof(T).Name);
      _instance = obj.AddComponent<T>();
      DontDestroyOnLoad(obj); // 跨场景保留
      return _instance;
    }
  }

  protected virtual void Awake()
  {
    if (_instance == null)
    {
      _instance = this as T;
      DontDestroyOnLoad(gameObject);
      OnInit(); // 让子类初始化
    }
    else if (_instance != this)
    {
      Destroy(gameObject); // 防止重复
    }
  }

  /// <summary>
  /// 子类初始化逻辑放这里
  /// </summary>
  protected virtual void OnInit()
  {
    //
  }
}