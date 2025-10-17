using UnityEngine;

public class DamagePopupMgr : SingletonMono<DamagePopupMgr>
{
  public Transform damagePopup;
  public Transform damagePopupParent;

  // // 测试鼠标点击处飘字，对象上需要添加Player Input
  // private void OnFire()
  // {
  //   if (UnityEngine.Camera.main != null)
  //   {
  //     var mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
  //     mousePosition.z = 0;
  //     DamagePopup.Create(mousePosition, Random.Range(1, 50));
  //   }
  // }
}