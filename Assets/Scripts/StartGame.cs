using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
  private void Start()
  {
    AudioClip clip = Resources.Load<AudioClip>("CasualGameSounds/356-8-bit-chiptune-game-music-357518");
    BGMManager.Instance.PlayBGM(clip);
    UIManager.Instance.ShowUI("UI/SettingUI", LayerIndex.Tip);
  }

  public void ClickStart()
  {
    Debug.Log("start game...");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}