using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/**
 * 收集
 */
public class ItemCollector : MonoBehaviour
{
  public Text cherriesText;
  public AudioSource audio;

  private int _cherries = 0;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Cherry"))
    {
      audio.Play();
      Destroy(other.gameObject);
      _cherries++;
      cherriesText.text = "Cherries: " + (GameManager.Instance.Score + _cherries);
      GameManager.Instance.TempScore = _cherries;
    }
  }
}