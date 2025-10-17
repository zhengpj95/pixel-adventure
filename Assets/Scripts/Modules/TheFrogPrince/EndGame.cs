using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
  public Text text;
  public Button restartBtn;
  public Button quitBtn;

  private void Start()
  {
    UIManager.Instance.HideUI("UI/ItemCollectorUI"); //
    Debug.Log("EndGame::HideUI...");
    AudioManager.Instance.Stop();
    text.text = "SCORE: " + TheFrogPrinceProxy.Instance.Score;
    restartBtn.onClick.AddListener(delegate
    {
      SceneManager.LoadScene(SceneUIName.Level1);
      TheFrogPrinceProxy.Instance.Score = 0;
      TheFrogPrinceProxy.Instance.TempScore = 0;
    });
    quitBtn.onClick.AddListener(delegate { Application.Quit(); });
  }

  private void OnDestroy()
  {
    restartBtn.onClick.RemoveAllListeners();
    quitBtn.onClick.RemoveAllListeners();
  }
}