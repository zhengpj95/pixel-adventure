using UnityEngine;

/**
 * 剑伤害范围碰撞
 */
public class PlayerSword : MonoBehaviour
{
  private Vector3 _position;

  private void Awake()
  {
    _position = transform.localPosition;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    var damageable = other.GetComponent<IDamageable>();
    if (damageable != null)
    {
      var position = transform.parent.position;
      Vector2 direction = other.transform.position - position;

      damageable.OnHit(1, direction.normalized * 1000);
    }
  }

  public void IsFacingRight(bool isRight)
  {
    if (isRight)
      transform.localPosition = _position;
    else
      transform.localPosition = new Vector3(-_position.x, _position.y, _position.z);
  }
}