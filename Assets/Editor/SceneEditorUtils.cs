using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneEditorUtils
{
  [MenuItem("Scene/打开游戏启动场景")]
  private static void OpenGameStartScene()
  {
    EditorSceneManager.OpenScene("Assets/Scenes/Start.scene", OpenSceneMode.Single);
    EditorApplication.isPlaying = true;
  }
}