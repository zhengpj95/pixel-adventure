using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMono<BGMManager>
{
  [Header("默认背景音乐")] 
  public AudioClip defaultBgm;

  [Header("音量设置")] 
  [Range(0f, 1f)] public float bgmVolume = 1f;
  [Range(0f, 1f)] public float sfxVolume = 1f;

  private AudioSource _bgmSource;
  private readonly List<AudioSource> _sfxSources = new List<AudioSource>();

  protected override void Awake()
  {
    base.Awake();

    _bgmSource = gameObject.GetComponent<AudioSource>();
    if (_bgmSource == null)
    {
      _bgmSource = gameObject.AddComponent<AudioSource>();
    }

    _bgmSource.loop = true;
    _bgmSource.playOnAwake = false;
    _bgmSource.volume = bgmVolume;

    if (defaultBgm != null)
    {
      PlayBGM(defaultBgm);
    }
  }

  #region === BGM ===

  public void PlayBGM(AudioClip clip)
  {
    if (clip == null) return;
    if (_bgmSource.clip == clip && _bgmSource.isPlaying) return;

    _bgmSource.clip = clip;
    _bgmSource.volume = bgmVolume;
    _bgmSource.Play();
  }

  public void StopBGM()
  {
    _bgmSource.Stop();
  }

  #endregion

  #region === SFX ===

  // ReSharper disable Unity.PerformanceAnalysis
  public void PlaySfx(AudioClip clip, bool loop = false)
  {
    if (clip == null) return;
    AudioSource sfx = gameObject.AddComponent<AudioSource>();
    sfx.clip = clip;
    sfx.loop = loop;
    sfx.volume = sfxVolume;
    sfx.Play();
    _sfxSources.Add(sfx);

    // 如果不是循环音效，则自动销毁
    if (!loop)
    {
      StartCoroutine(DestroyAfterPlay(sfx));
    }
  }

  private IEnumerator DestroyAfterPlay(AudioSource sfx)
  {
    yield return new WaitForSeconds(sfx.clip.length);
    _sfxSources.Remove(sfx);
    Destroy(sfx);
  }

  public void StopAllSfx()
  {
    foreach (var sfx in _sfxSources)
    {
      if (sfx != null)
      {
        sfx.Stop();
      }
    }
  }

  #endregion
}