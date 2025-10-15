using UnityEngine;

/**
 * 收集
 */
public class ItemCollector : MonoBehaviour
{
  private int _cherries;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Cherry"))
    {
      Destroy(other.gameObject);
      _cherries++;
      MessageCenter.Dispatch(GameEvent.Cherries, TheFrogPrinceProxy.Instance.Score + _cherries);
      TheFrogPrinceProxy.Instance.TempScore = _cherries;
    }
  }
}