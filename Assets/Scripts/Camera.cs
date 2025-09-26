using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 相机跟随人物
 */
public class Camera : MonoBehaviour
{
  public Transform player;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
  }
}