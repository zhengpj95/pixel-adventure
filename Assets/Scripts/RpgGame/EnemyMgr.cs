using UnityEngine;
using Random = UnityEngine.Random;

/**
 * 定时随机生成enemy
 */
public class EnemyManager : MonoBehaviour
{
  public EnemyMovement enemyPrefab;
  public float interval = 3f;
  public int maxEnemies = 5;
  public Transform enemyParent;

  private int _enemyCount;
  private float _timer;

  private void Update()
  {
    _timer += Time.deltaTime;
    if (_timer >= interval)
    {
      _timer = 0f;
      // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
      SpawnEnemy();
    }
  }

  private void OnEnable()
  {
    MessageCenter.AddListener(GameEvent.EnemyDeath, OnEnemyDeath);
  }

  private void SpawnEnemy()
  {
    if (_enemyCount >= maxEnemies) return;

    var pos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
    var obj = PoolManager.Instance.Alloc(enemyPrefab, enemyParent);
    obj.gameObject.transform.position = pos;
    obj.InitHp(Random.Range(10, 21));
    _enemyCount++;
  }

  private void OnEnemyDeath()
  {
    _enemyCount--;
  }
}