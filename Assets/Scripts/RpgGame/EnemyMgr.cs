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
      SpawnEnemy();
    }
  }

  private void OnEnable()
  {
    MessageCenter.AddListener(GameEvent.EnemyDeath, OnEnemyDeath);
  } // ReSharper disable Unity.PerformanceAnalysis
  private void SpawnEnemy()
  {
    if (_enemyCount >= maxEnemies) return;

    var pos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
    var obj = Instantiate(enemyPrefab, enemyParent);
    obj.transform.position = pos;
    var character = obj.gameObject.GetComponent<DamageableCharacter>();
    if (character) character.health = 20;
    _enemyCount++;
  }

  private void OnEnemyDeath()
  {
    _enemyCount--;
    Debug.Log("debug:: " + _enemyCount);
  }
}