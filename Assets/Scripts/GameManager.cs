using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
  private static GameManager _instance;

  public static GameManager Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = new GameManager();
      }

      return _instance;
    }
  }

  public int Score = 0;
  public int TempScore = 0; // 每关卡得到的分数
}