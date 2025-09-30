using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 对象池管理
 */
public class PoolManager : SingletonMono<PoolManager>
{
  private Dictionary<string, object> _pools;

  protected override void Awake()
  {
    base.Awake();
    _pools = new Dictionary<string, object>();
  }

  // 创建池
  public void CreatePool<T>(T prefab, int initialCount) where T : MonoBehaviour
  {
    var key = prefab.name;
    if (!_pools.ContainsKey(key))
    {
      _pools[key] = new PoolObject<T>(prefab, initialCount);
    }
  }

  // 获取对象，可指定父节点
  public T Alloc<T>(T prefab, Transform parent = null) where T : MonoBehaviour
  {
    var key = prefab.name;
    if (_pools.TryGetValue(key, out var pool))
    {
      return ((PoolObject<T>)pool).Alloc(parent);
    }

    // 没有池时自动创建一个
    var singlePool = new PoolObject<T>(prefab, 1);
    _pools[key] = singlePool;
    return singlePool.Alloc(parent);
  }

  // 归还对象
  public void Release<T>(T obj, Transform defaultParent = null) where T : MonoBehaviour
  {
    var key = obj.name.Replace("(Clone)", "").Trim();
    if (_pools.TryGetValue(key, out var pool))
    {
      ((PoolObject<T>)pool).Release(obj, defaultParent);
    }
    else
    {
      Destroy(obj.gameObject); // 没有池就直接销毁
    }
  }
}