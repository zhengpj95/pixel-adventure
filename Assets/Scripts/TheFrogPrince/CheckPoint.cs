using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
  private AudioSource _audio;
  private bool _levelComplete = false;

  public void Start()
  {
    _audio = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Player" && !_levelComplete)
    {
      // 每关到达终点时候，再计算总分
      GameManager.Instance.Score += GameManager.Instance.TempScore;
      GameManager.Instance.TempScore = 0;
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