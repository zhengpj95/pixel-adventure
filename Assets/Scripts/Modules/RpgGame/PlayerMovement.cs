using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 500f;

  private Animator _animator;
  private Vector2 _moveInput;
  private Rigidbody2D _rb2d;
  private SpriteRenderer _sprite;

  private void Start()
  {
    _rb2d = GetComponent<Rigidbody2D>();
    _sprite = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();
  }

  private void FixedUpdate()
  {
    _rb2d.AddForce(_moveInput * moveSpeed);
  }

  public void OnMove(InputValue value)
  {
    _moveInput = value.Get<Vector2>();

    // 设置idle和walk
    if (_moveInput != Vector2.zero)
    {
      _animator.SetBool("isWalking", true);
      // 设置翻转
      if (_moveInput.x > 0f)
      {
        _sprite.flipX = false;
        gameObject.BroadcastMessage("IsFacingRight", true);
      }
      else if (_moveInput.x < 0f)
      {
        _sprite.flipX = true;
        gameObject.BroadcastMessage("IsFacingRight", false);
      }
    }
    else
    {
      _animator.SetBool("isWalking", false);
    }
  }

  private void OnFire()
  {
    _animator.SetTrigger("swordAttack");
  }

  public void OnDamage()
  {
    // _animator.SetTrigger("isDamage");
  }

  public void OnDie()
  {
    _animator.SetTrigger("isDie");
  }
}