using UnityEngine;

public class BGMManager : MonoBehaviour
{
  public static BGMManager Instance { get; private set; }
  private AudioSource _audioSource;

  public AudioClip defaultClip;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject); // 不销毁

    _audioSource = gameObject.GetComponent<AudioSource>();
    if (_audioSource == null)
      _audioSource = gameObject.AddComponent<AudioSource>();

    _audioSource.loop = true;
    _audioSource.playOnAwake = false;

    if (defaultClip != null)
    {
      PlayBGM(defaultClip);
    }
  }

  public void PlayBGM(AudioClip clip)
  {
    if (clip == null) return;
    if (_audioSource.clip == clip && _audioSource.isPlaying) return;

    _audioSource.clip = clip;
    _audioSource.Play();
  }

  public void StopBGM()
  {
    _audioSource.Stop();
  }
}