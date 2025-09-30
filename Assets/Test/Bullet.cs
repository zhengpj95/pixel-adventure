using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
  public float speed;

  private Rigidbody2D _rigidbody;
  private Vector3 _dir;

  // 发射逻辑
  public void Launch(Vector3 dir)
  {
    _rigidbody.velocity = dir.normalized * speed;
  }

  public void OnReset()
  {
    _rigidbody.velocity = Vector2.zero;
    transform.position = Vector2.zero;
  }

  private void Awake()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    // 碰撞后归还对象池
    PoolManager.Instance.Release(this);
  }
}