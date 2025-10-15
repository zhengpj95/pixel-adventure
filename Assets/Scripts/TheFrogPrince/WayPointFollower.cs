using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 两点范围内移动
 */
public class WayPointFollower : MonoBehaviour
{
  public GameObject[] waypoints;
  public float speed = 2f;

  private int _currentWayPointIndex = 0;

  public void Update()
  {
    if (Vector2.Distance(waypoints[_currentWayPointIndex].transform.position, transform.position) < .1f)
    {
      _currentWayPointIndex++;
      if (_currentWayPointIndex >= waypoints.Length)
      {
        _currentWayPointIndex = 0;
      }
    }

    transform.position = Vector2.MoveTowards(transform.position, waypoints[_currentWayPointIndex].transform.position,
      speed * Time.deltaTime);
  }
}