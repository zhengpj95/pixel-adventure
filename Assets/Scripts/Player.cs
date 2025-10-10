using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
  private Rigidbody2D _rb2d;
  private BoxCollider2D _coll;
  private SpriteRenderer _sprite;
  private Animator _anim;

  private float _dirX = 0f;

  public float moveSpeed = 7f;
  public float jumpForce = 7f;
  public LayerMask jumpableGround;

  private AudioClip _jumpSound;

  private enum MovementState
  {
    Idle,
    Running,
    Jumping,
    Falling,
  }

  private void Awake()
  {
    // 创建角色后，播放音乐
    AudioClip clip = Resources.Load<AudioClip>("CasualGameSounds/mobile-casual-video-game-music-158301");
    BGMManager.Instance.PlayBGM(clip);

    AudioClip sfx = Resources.Load<AudioClip>("CasualGameSounds/cartoon-jump-6462");
    _jumpSound = sfx;
  }

  public void Start()
  {
    UIManager.Instance.ShowUI("UI/ItemCollectorUI", LayerIndex.Model);

    _rb2d = GetComponent<Rigidbody2D>();
    _coll = GetComponent<BoxCollider2D>();
    _sprite = GetComponent<SpriteRenderer>();
    _anim = GetComponent<Animator>();
  }

  public void Update()
  {
    _dirX = Input.GetAxisRaw("Horizontal");
    _rb2d.velocity = new Vector2(_dirX * moveSpeed, _rb2d.velocity.y);

    if (Input.GetButtonDown("Jump") && IsGrounded())
    {
      BGMManager.Instance.PlaySfx(_jumpSound);
      _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpForce);
    }

    UpdateAnimationState();
  }

  private void UpdateAnimationState()
  {
    MovementState state;
    if (_dirX > 0f)
    {
      state = MovementState.Running;
      _sprite.flipX = false;
    }
    else if (_dirX < 0f)
    {
      state = MovementState.Running;
      _sprite.flipX = true;
    }
    else
    {
      state = MovementState.Idle;
    }

    if (_rb2d.velocity.y > .1f)
    {
      state = MovementState.Jumping;
    }
    else if (_rb2d.velocity.y < -.1f)
    {
      state = MovementState.Falling;
    }

    _anim.SetInteger("state", (int)state);
  }

  private bool IsGrounded()
  {
    return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
  }
}