using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TestPool : MonoBehaviour
{
  public Bullet bulletPrefab;
  public GameObject bullets;
  private Collider2D _collider2D;

  private void Start()
  {
    _collider2D = gameObject.GetComponent<Collider2D>();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      Bullet b = PoolManager.Instance.Alloc(bulletPrefab, bullets.transform);
      b.transform.position = gameObject.transform.position;
      // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
      Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), _collider2D);
      b.Launch(Vector3.right);
    }
  }
}