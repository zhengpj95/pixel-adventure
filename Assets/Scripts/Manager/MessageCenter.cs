using System;
using System.Collections.Generic;

/// <summary>
/// 游戏全局消息中心（字符串 Key 版）
/// 支持泛型参数
/// </summary>
public static class MessageCenter
{
  private static readonly Dictionary<string, Delegate> eventTable = new();

  /// <summary>
  /// 添加监听
  /// </summary>
  public static void AddListener<T>(string eventName, Action<T> listener)
  {
    if (!eventTable.ContainsKey(eventName))
    {
      eventTable[eventName] = null;
    }

    eventTable[eventName] = (Action<T>)eventTable[eventName] + listener;
  }

  /// <summary>
  /// 移除监听
  /// </summary>
  public static void RemoveListener<T>(string eventName, Action<T> listener)
  {
    if (eventTable.ContainsKey(eventName))
    {
      eventTable[eventName] = (Action<T>)eventTable[eventName] - listener;
    }
  }

  /// <summary>
  /// 派发事件
  /// </summary>
  public static void Dispatch<T>(string eventName, T arg)
  {
    if (eventTable.ContainsKey(eventName))
    {
      var action = eventTable[eventName] as Action<T>;
      action?.Invoke(arg);
    }
  }

  /// <summary>
  /// 清空所有事件（场景切换时调用）
  /// </summary>
  public static void ClearAll()
  {
    eventTable.Clear();
  }
}