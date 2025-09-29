using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
  public Text text;

  public void Show()
  {
    gameObject.SetActive(true);
  }

  // ReSharper disable Unity.PerformanceAnalysis
  public void SetProgress(float progress)
  {
    Debug.Log("LoadingUI::SetProgress " + progress);
    if (text)
    {
      text.text = progress.ToString("0.00");
    }
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}