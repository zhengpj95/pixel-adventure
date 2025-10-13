using UnityEngine;

/**
 * 相机跟随人物
 */
public class Camera : MonoBehaviour
{
  public Transform player;

  private void Update()
  {
    if (player) transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
  }
}