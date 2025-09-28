using UnityEditor;
using UnityEngine;
using System.IO;


public static class SceneNameGenerator
{
  private const string FilePath = "Assets/Scripts/SceneUIName.cs";

  [MenuItem("Scene/生成SceneUIName")]
  public static void Generate()
  {
    var scenes = EditorBuildSettings.scenes;
    if (scenes.Length == 0)
    {
      Debug.LogWarning("⚠ 没有找到任何场景，请先在 Build Settings 里添加！");
      return;
    }

    string code = "/**\n";
    code += " * 此文件自动生成，请勿手动修改\n";
    code += " */\n";
    code += "public static class SceneUIName\n";
    code += "{\n";

    foreach (var scene in scenes)
    {
      if (scene.enabled)
      {
        string sceneName = Path.GetFileNameWithoutExtension(scene.path);
        code += $"  public const string {sceneName} = \"{sceneName}\";\n";
      }
    }

    code += "}\n";

    File.WriteAllText(FilePath, code);
    AssetDatabase.Refresh();

    Debug.Log($"✅ SceneNames.cs 已生成，路径：{FilePath}");
  }
}