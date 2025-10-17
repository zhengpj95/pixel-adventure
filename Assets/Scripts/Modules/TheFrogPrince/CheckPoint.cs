using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
  private AudioSource _audio;
  private bool _levelComplete;

  public void Start()
  {
    _audio = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Player" && !_levelComplete)
    {
      // 每关到达终点时候，再计算总分
      TheFrogPrinceProxy.Instance.Score += TheFrogPrinceProxy.Instance.TempScore;
      TheFrogPrinceProxy.Instance.TempScore = 0;
      _audio?.Play();
      _levelComplete = true;
      Invoke("CompleteLevel", 2f);
    }
  }

  private void CompleteLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}