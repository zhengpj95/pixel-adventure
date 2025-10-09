using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 相机跟随人物
 */
public class Camera : MonoBehaviour
{
  public Transform player;

  private void Update()
  {
    transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
  }
}