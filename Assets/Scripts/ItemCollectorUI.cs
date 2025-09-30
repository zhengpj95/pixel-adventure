using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectorUI : MonoBehaviour
{
  public Text cherriesText;
  public AudioSource audio;

  private void OnEnable()
  {
    cherriesText.text = "0";
    MessageCenter.AddListener<int>(GameEvent.Cherries, OnCherriesUpdate);
  }

  private void OnDisable()
  {
    MessageCenter.RemoveListener<int>(GameEvent.Cherries, OnCherriesUpdate);
  }

  private void OnCherriesUpdate(int cherries)
  {
    audio.Play();
    cherriesText.text = cherries.ToString();
  }
}