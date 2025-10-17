using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
  private const float DisappearTimerMax = 1f;
  private static int _sortingOrder;
  public float disappearTimer = 2f;
  public float disappearSpeed = 1f;

  private Vector3 _moveVector;
  private Color _textColor;
  private TextMeshPro _textMeshPro;

  private void Awake()
  {
    _textMeshPro = transform.GetComponent<TextMeshPro>();
    _textColor = _textMeshPro.color;
  }

  private void Update()
  {
    transform.position += _moveVector * Time.deltaTime;

    // 修改飘字动画效果，速度放大，然后缩小
    _moveVector -= _moveVector * (7 * Time.deltaTime);
    if (disappearTimer > DisappearTimerMax * 0.5f)
    {
      const float increaseScaleAmount = 1f;
      transform.localScale += Vector3.one * (increaseScaleAmount * Time.deltaTime);
    }
    else
    {
      const float increaseScaleAmount = 1f;
      transform.localScale -= Vector3.one * (increaseScaleAmount * Time.deltaTime);
    }

    // 逐渐修改透明度，直至销毁
    disappearTimer -= Time.deltaTime;
    if (disappearTimer <= 0)
    {
      _textColor.a -= disappearSpeed * Time.deltaTime;
      _textMeshPro.color = _textColor;
      if (_textColor.a <= 0) Destroy(gameObject);
    }
  }

  public static DamagePopup Create(Vector3 position, int damageValue, bool isCriticalHit = false)
  {
    var damagePopupTransform = Instantiate(DamagePopupMgr.Instance.damagePopup, position, quaternion.identity,
      DamagePopupMgr.Instance.damagePopupParent);
    var damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
    damagePopup.SetUp(damageValue, isCriticalHit);
    return damagePopup;
  }

  private void SetUp(int damageValue, bool isCriticalHit = false)
  {
    _textMeshPro.SetText(damageValue.ToString());
    if (!isCriticalHit)
    {
      _textMeshPro.fontSize = 5;
    }
    else
    {
      _textMeshPro.fontSize = 7;
      _textColor = Color.red;
      _textMeshPro.color = _textColor;
    }

    disappearTimer = DisappearTimerMax;
    _moveVector = new Vector3(0.7f, 1f, 0f) * 10;
    _sortingOrder++;
    _textMeshPro.sortingOrder = _sortingOrder;
  }
}