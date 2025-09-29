using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
  public string firstSceneName = "";
  public LoadingUI loadingUI;

  public void Start()
  {
    StartCoroutine(LoadSceneRoutine(firstSceneName));
  }
  
  private IEnumerator LoadSceneRoutine(string sceneName)
  {
    loadingUI.Show();

    AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
    op.allowSceneActivation = true; // 允许切换（可手动控制）

    while (!op.isDone)
    {
      // 更新进度条
      loadingUI.SetProgress(op.progress);
      yield return null;
    }

    // 场景加载完成后隐藏
    loadingUI.Hide();
  }
}