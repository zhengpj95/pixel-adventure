using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
  private void Start()
  {
    UIManager.Instance.ShowUI("UI/SettingUI", LayerIndex.Tip);
  }

  public void ClickStart()
  {
    Debug.Log("start game...");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}