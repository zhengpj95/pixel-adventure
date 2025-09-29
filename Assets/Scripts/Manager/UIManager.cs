using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum LayerIndex
{
  Main = 0,
  Window = 1,
  Model = 2,
  Tip = 3,
}

public class UIManager : SingletonMono<UIManager>
{
  public Transform mainLayer;
  public Transform windowLayer;
  public Transform modelLayer;
  public Transform tipLayer;

  private Dictionary<string, GameObject> _uiCache;

  protected override void Awake()
  {
    base.Awake();
    _uiCache = new Dictionary<string, GameObject>();
  }

  public GameObject ShowUI(string prefabPath, LayerIndex layer)
  {
    if (!_uiCache.ContainsKey(prefabPath))
    {
      GameObject prefab = Resources.Load<GameObject>(prefabPath);
      Debug.Log("11111 + " + prefabPath);
      Debug.Log(prefab);
      if (prefab == null)
      {
        Debug.LogError("prefab is null");
        return null;
      }

      Transform parent = layer switch
      {
        LayerIndex.Main => mainLayer,
        LayerIndex.Window => windowLayer,
        LayerIndex.Model => modelLayer,
        LayerIndex.Tip => tipLayer,
        _ => mainLayer
      };
      GameObject go = Instantiate(prefab, parent);
      _uiCache[prefabPath] = go;
    }

    _uiCache[prefabPath].SetActive(true);
    _uiCache[prefabPath].transform.SetAsLastSibling(); // 保证在父层级的最上方显示
    return _uiCache[prefabPath];
  }

  public void HideUI(string prefabPath)
  {
    if (_uiCache.ContainsKey(prefabPath))
      _uiCache[prefabPath].SetActive(false);
  }
}