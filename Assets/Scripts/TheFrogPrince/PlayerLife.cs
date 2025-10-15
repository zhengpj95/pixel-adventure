using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 角色声明周期，死亡处理和复活
 */
public class PlayerLife : MonoBehaviour
{
  public AudioSource deathSound;
  private Animator _anim;

  private Rigidbody2D _rb;

  public void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponent<Animator>();
  }

  private void Update()
  {
    if (transform.position.y < -10) RestartLevel();
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
    MessageCenter.Dispatch(GameEvent.Cherries, TheFrogPrinceProxy.Instance.Score); // 死亡重置分数
  }
}