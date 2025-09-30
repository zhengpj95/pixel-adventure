using System.Collections.Generic;
using UnityEngine;


// 可选接口，用于对象自定义重置逻辑
public interface IPoolable
{
  void OnReset(); // 返回对象池时调用
}

/**
 * 某类对象池
 */
public class PoolObject<T> where T : MonoBehaviour
{
  private readonly T _prefab;
  private readonly Queue<T> _pool = new Queue<T>();

  public int Count => _pool.Count;

  public PoolObject(T prefab, int initialCount)
  {
    _prefab = prefab;

    for (var i = 0; i < initialCount; i++)
    {
      var obj = Object.Instantiate(prefab, null, false);
      obj.gameObject.SetActive(false);
      _pool.Enqueue(obj);
    }
  }

  public T Alloc(Transform parent = null)
  {
    var obj = _pool.Count > 0 ? _pool.Dequeue() : Object.Instantiate(_prefab, parent, false);

    obj.gameObject.SetActive(true);
    if (parent)
    {
      obj.transform.SetParent(parent, worldPositionStays: false);
    }

    ResetObject(obj);
    return obj;
  }

  public void Release(T obj, Transform defaultParent = null)
  {
    obj.gameObject.SetActive(false);

    // 回到默认父节点
    if (defaultParent)
    {
      obj.transform.SetParent(defaultParent, worldPositionStays: false);
    }

    ResetObject(obj);
    _pool.Enqueue(obj);
  }

  private void ResetObject(T obj)
  {
    // 重置位置、旋转、缩放或状态
    // obj.transform.localPosition = Vector3.zero;
    // obj.transform.localRotation = Quaternion.identity;
    // obj.transform.localScale = Vector3.one;

    // 如果对象实现了 IPoolable 接口，可调用自定义重置
    if (obj is IPoolable poolObj)
    {
      poolObj.OnReset();
    }
  }
}