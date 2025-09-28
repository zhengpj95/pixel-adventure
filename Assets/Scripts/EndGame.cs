using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
  public Text text;
  public Button restartBtn;
  public Button quitBtn;

  private void Start()
  {
    BGMManager.Instance.StopBGM();
    text.text = "SCORE: " + GameManager.Instance.Score;
    restartBtn.onClick.AddListener(delegate
    {
      SceneManager.LoadScene(SceneUIName.Level1);
      GameManager.Instance.Score = 0;
      GameManager.Instance.TempScore = 0;
    });
    quitBtn.onClick.AddListener(delegate { Application.Quit(); });
  }

  private void OnDestroy()
  {
    restartBtn.onClick.RemoveAllListeners();
    quitBtn.onClick.RemoveAllListeners();
  }
}