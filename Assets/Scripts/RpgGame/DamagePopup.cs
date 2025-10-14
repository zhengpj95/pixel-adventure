using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
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
    var damagePopupTransform = Instantiate(DamagePopupMgr.Instance.damagePopup, position, quaternion.identity);
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

    _moveVector = new Vector3(0.7f, 1f, 0f);
    _sortingOrder++;
    _textMeshPro.sortingOrder = _sortingOrder;
  }
}