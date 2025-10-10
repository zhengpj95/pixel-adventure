using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
  [Header("默认背景音乐")] 
  public AudioClip defaultBgm;

  [Header("音量设置")] 
  [Range(0f, 1f)] public float bgmVolume = 1f;
  [Range(0f, 1f)] public float sfxVolume = 1f;

  // 背景音乐
  private AudioSource _bgmSource;

  // 正在播放的音效，包含循环音效
  private readonly List<AudioSource> _sfxSources = new();

  // 正在播放的循环音效
  private readonly Dictionary<string, AudioSource> _activeSfx = new();

  // 音效对象池
  private readonly Queue<AudioSource> _sfxPool = new();
  private const int InitialSfxPoolSize = 5;

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

    // 初始化音效对象池
    for (var i = 0; i < InitialSfxPoolSize; i++)
    {
      CreateSfxSource();
    }

    // 初始背景音乐不为空，则开始播放
    if (defaultBgm != null)
    {
      PlayBGM(defaultBgm);
    }
  }

  // sfx对象池
  private AudioSource CreateSfxSource()
  {
    var obj = new GameObject("SFX_AudioSource");
    obj.transform.SetParent(transform);
    var src = obj.AddComponent<AudioSource>();
    src.playOnAwake = false;
    _sfxPool.Enqueue(src);
    return src;
  }

  // 分配sfx
  private AudioSource AllocSfxSource()
  {
    return _sfxPool.Count > 0 ? _sfxPool.Dequeue() : CreateSfxSource();
  }

  // 放回对象池
  private void ReleaseSfxSource(AudioSource src)
  {
    src.Stop();
    src.clip = null;
    src.loop = false;
    src.volume = sfxVolume;
    _sfxPool.Enqueue(src);
  }

  // 暂停背景音乐和所有音效
  public void Stop()
  {
    StopBGM();
    StopAllSfx();
  }

  #region === BGM ===

  public void PlayBGM(string soundType)
  {
    var clip = Resources.Load<AudioClip>("Sound/" + soundType);
    PlayBGM(clip);
  }

  private void PlayBGM(AudioClip clip)
  {
    if (clip == null) return;
    if (_bgmSource.clip == clip && _bgmSource.isPlaying) return;

    StopAllCoroutines();
    StartCoroutine(FadeInBGM(clip));
  }

  private void StopBGM(float fadeTime = 1f)
  {
    StopAllCoroutines();
    StartCoroutine(FadeOutBGM(fadeTime));
  }

  private IEnumerator FadeInBGM(AudioClip clip, float fadeTime = 1f)
  {
    _bgmSource.clip = clip;
    _bgmSource.volume = 0f;
    _bgmSource.Play();

    float t = 0f;
    while (t < fadeTime)
    {
      t += Time.deltaTime;
      _bgmSource.volume = Mathf.Lerp(0f, bgmVolume, t / fadeTime);
      yield return null;
    }

    _bgmSource.volume = bgmVolume;
  }

  private IEnumerator FadeOutBGM(float fadeTime = 1f)
  {
    float startVol = _bgmSource.volume;
    float t = 0f;
    while (t < fadeTime)
    {
      t += Time.deltaTime;
      _bgmSource.volume = Mathf.Lerp(startVol, 0f, t / fadeTime);
      yield return null;
    }

    _bgmSource.Stop();
    _bgmSource.clip = null;
    _bgmSource.volume = bgmVolume;
  }

  #endregion =====================

  #region === SFX ===

  public void PlaySfx(string soundType, bool loop = false)
  {
    var clip = Resources.Load<AudioClip>("Sound/" + soundType);
    PlaySfx(clip, soundType, loop);
  }

  // ReSharper disable Unity.PerformanceAnalysis
  private void PlaySfx(AudioClip clip, string soundType, bool loop = false)
  {
    if (clip == null) return;
    if (loop && _activeSfx.ContainsKey(soundType)) return; // 如果已经有同类型的循环音效在播放，直接返回

    var sfx = AllocSfxSource();
    sfx.clip = clip;
    sfx.loop = loop;
    sfx.volume = sfxVolume;
    sfx.Play();
    _sfxSources.Add(sfx); // 正在播放的音效

    // 如果不是循环音效，则自动销毁
    if (!loop)
    {
      StartCoroutine(DestroyAfterPlay(sfx));
    }
    else
    {
      _activeSfx.Add(soundType, sfx);
    }
  }

  private IEnumerator DestroyAfterPlay(AudioSource sfx)
  {
    yield return new WaitWhile(() => sfx.isPlaying);
    _sfxSources.Remove(sfx);
    ReleaseSfxSource(sfx);
  }

  // 停止播放全部音效
  private void StopAllSfx()
  {
    // 循环音效
    foreach (var (key, src) in _activeSfx)
    {
      src.Stop();
      _activeSfx.Remove(key);
      _sfxSources.Remove(src);
      ReleaseSfxSource(src);
    }

    // 全部音效
    foreach (var sfx in _sfxSources.ToList())
    {
      if (sfx == null) continue;
      sfx.Stop();
      _sfxSources.Remove(sfx);
      ReleaseSfxSource(sfx);
    }
  }

  // 停止播放循环音效
  public void StopSfx(string soundType)
  {
    if (_activeSfx.TryGetValue(soundType, out AudioSource src))
    {
      src.Stop();
      _activeSfx.Remove(soundType);
      _sfxSources.Remove(src);
      ReleaseSfxSource(src);
    }
  }

  #endregion =====================
}