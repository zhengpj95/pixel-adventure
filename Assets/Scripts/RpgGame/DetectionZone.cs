using UnityEngine;

/**
 * 探测区域，控制 enemy 等向 player 移动
 */
public class DetectionZone : MonoBehaviour
{
  [Tooltip("探测半径")] public float viewRadius;

  [Tooltip("探测层级")] public LayerMask layerMask;

  // 监控的对象，玩家
  public Collider2D DetectedObj { get; private set; }

  private void Update()
  {
    var c2d = Physics2D.OverlapCircle(transform.position, viewRadius, layerMask);
    if (c2d) DetectedObj = c2d;
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, viewRadius);
  }
}