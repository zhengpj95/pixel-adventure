using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  public float moveSpeed = 20f;

  private Animator _animator;
  private Rigidbody2D _rb2d;
  private SpriteRenderer _sprite;
  private DetectionZone _zone;

  private void Start()
  {
    _rb2d = GetComponent<Rigidbody2D>();
    _sprite = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();
    _zone = GetComponent<DetectionZone>();
  }

  private void FixedUpdate()
  {
    if (_zone?.DetectedObj)
    {
      // if (Vector2.Distance(_zone.detectedObjs.transform.position, transform.position) < 1f)
      // {
      //   OnWalkStop(); // 近距离就不移动了
      //   return;
      // }

      var direction = _zone.DetectedObj.transform.position - transform.position; // 我指向玩家的向量
      if (direction.magnitude <= _zone.viewRadius)
      {
        _rb2d.AddForce(direction.normalized * moveSpeed);
        OnWalk();
        if (direction.x > 0)
          _sprite.flipX = false;
        else if (direction.x < 0)
          _sprite.flipX = true;
      }
      else
      {
        OnWalkStop();
      }
    }
    else
    {
      OnWalkStop();
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    // 碰撞伤害
    var collider = other.collider;
    var damageable = collider.GetComponent<IDamageable>();
    if (damageable != null && collider.CompareTag("Player"))
    {
      Vector2 direction = collider.transform.position - transform.position;
      var force = direction.normalized * 100;
      var damage = Random.Range(1, 10);
      damageable.OnHit(damage, force);
    }
  }

  private void OnWalk()
  {
    _animator.SetBool("isWalking", true);
  }

  private void OnWalkStop()
  {
    _animator.SetBool("isWalking", false);
  }

  public void OnDamage()
  {
    _animator.SetTrigger("isDamage");
  }

  public void OnDie()
  {
    _animator.SetTrigger("isDie");
  }
}