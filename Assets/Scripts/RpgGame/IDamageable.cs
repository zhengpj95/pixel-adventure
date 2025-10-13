using UnityEngine;

/**
 * 可被攻击的
 */
public interface IDamageable
{
  public void OnHit(int damage, Vector2 knockback);
}