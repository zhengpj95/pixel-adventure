using UnityEngine;

public class HpBar : MonoBehaviour
{
  public Transform fill; // 红色条
  private int _maxHp;
  private float _maxWidth;

  private void Awake()
  {
    if (fill == null)
      fill = transform.Find("fill");
    if (fill != null)
      _maxWidth = fill.localScale.x;
  }

  public void InitHp(int hp)
  {
    _maxHp = hp;
    SetHp(hp);
  }

  public void SetHp(float current)
  {
    if (!fill || _maxHp <= 0) return;

    var ratio = Mathf.Clamp01(current / _maxHp);
    var scale = fill.localScale;
    scale.x = _maxWidth * ratio;
    fill.localScale = scale;
  }
}