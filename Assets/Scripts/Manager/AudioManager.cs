using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
  [Header("默认背景音乐")] 
  public AudioClip defaultBgm;

  [Header("音量设置")] 
  [Range(0f, 1f)] public float bgmVolume = 1f;
  [Range(0f, 1f)] public float sfxVolume = 1f;

  private AudioSource _bgmSource;
  private readonly List<AudioSource> _sfxSources = new();

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

  private void PlayBGM(AudioClip clip)
  {
    if (clip == null) return;
    if (_bgmSource.clip == clip && _bgmSource.isPlaying) return;

    _bgmSource.clip = clip;
    _bgmSource.volume = bgmVolume;
    _bgmSource.Play();
  }

  public void PlayBGM(string soundType)
  {
    var clip = Resources.Load<AudioClip>("Sound/" + soundType);
    PlayBGM(clip);
  }

  public void StopBGM()
  {
    _bgmSource.Stop();
  }

  #endregion =====================

  #region === SFX ===

  // ReSharper disable Unity.PerformanceAnalysis
  private void PlaySfx(AudioClip clip, bool loop = false)
  {
    if (clip == null) return;
    var sfx = gameObject.AddComponent<AudioSource>();
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

  public void PlaySfx(string soundType)
  {
    var clip = Resources.Load<AudioClip>("Sound/" + soundType);
    PlaySfx(clip);
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

  #endregion =====================

  public void Stop()
  {
    StopBGM();
    StopAllSfx();
  }
}