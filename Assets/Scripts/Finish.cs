using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
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