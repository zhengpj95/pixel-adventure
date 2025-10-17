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
    // 检测某个圆形范围内是否存在碰撞体（Collider2D）。指定检测的层（Layer），默认检测所有层。
    var c2d = Physics2D.OverlapCircle(transform.position, viewRadius, layerMask);
    if (c2d) DetectedObj = c2d;
  }

  // 敌人画个圆，查看侦测范围
  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, viewRadius);
  }
}