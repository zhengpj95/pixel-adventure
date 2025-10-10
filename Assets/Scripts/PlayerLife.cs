using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/**
 * 角色声明周期，死亡处理和复活
 */
public class PlayerLife : MonoBehaviour
{
  public AudioSource deathSound;

  private Rigidbody2D _rb;
  private Animator _anim;

  public void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponent<Animator>();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Trap"))
    {
      deathSound.Play();
      Die();
    }
  }

  private void Die()
  {
    _rb.bodyType = RigidbodyType2D.Static;
    _anim.SetTrigger("death");
  }

  private void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  private void Update()
  {
    if (transform.position.y < -10)
    {
      RestartLevel();
    }
  }
}