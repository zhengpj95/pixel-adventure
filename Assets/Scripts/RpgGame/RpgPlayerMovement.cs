using UnityEngine;
using UnityEngine.InputSystem;

public class RpgPlayerMovement : MonoBehaviour
{
  public float moveSpeed = 5f;
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

    // 设置翻转
    if (_moveInput.x > 0f)
      _sprite.flipX = false;
    else if (_moveInput.x < 0f) _sprite.flipX = true;

    // 设置idle和walk
    if (_moveInput != Vector2.zero)
      _animator.SetBool("isWalking", true);
    else
      _animator.SetBool("isWalking", false);
  }

  public void OnMove(InputValue value)
  {
    _moveInput = value.Get<Vector2>();
  }
}