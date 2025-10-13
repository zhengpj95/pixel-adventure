using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
  public int health;

  private Rigidbody2D _rigidbody;

  private bool _targetable;

  private int Health
  {
    get => health;
    set
    {
      health = value;
      if (health <= 0)
      {
        gameObject.BroadcastMessage("OnDie");
        Targetable = false;
      }
      else
      {
        gameObject.BroadcastMessage("OnDamage");
      }
    }
  }

  private bool Targetable
  {
    get => _targetable;
    set
    {
      _targetable = value;
      if (!_targetable) _rigidbody.simulated = false; // 该刚体 不再参与任何物理计算。角色死亡后不再响应碰撞
    }
  }

  private void Awake()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  public void OnHit(int damage, Vector2 knockback)
  {
    Health -= damage;
    _rigidbody.AddForce(knockback);
  }

  private void OnObjectDestroyed()
  {
    Destroy(gameObject);
  }
}