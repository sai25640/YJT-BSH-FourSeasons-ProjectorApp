using System.Collections;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;
using UnityEngine;

/// <summary>
/// 可以搜索外部文件夹并循环播放音乐的工具类
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioLoopUtils : MonoBehaviour
{
    private List<AudioClip> audioClips;
    private AudioSource audioSource;
    private int index = -1;
    private string filePath;

    public void Awake()
    {
        audioClips = new List<AudioClip>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 1f;
        filePath = Application.streamingAssetsPath;
        LoadAudioSorce();
    }


    private void LoadAudioSorce()
    {
        string[] infos = Directory.GetFiles(string.Format("{0}/Mp3", filePath));
        string wavPath = string.Format("{0}/Wav", filePath);
        if (!File.Exists(wavPath))
        {
            var dir = Directory.CreateDirectory(wavPath);
            Mp3ToWav(infos);
        }

        string[] wavInfos = Directory.GetFiles(string.Format("{0}/Wav", filePath));
        if (wavInfos.Length <= 0)
        {
            Mp3ToWav(infos);
        }

        LoadWav(wavInfos);
    }

    private void Mp3ToWav(string[] urls)
    {
        for (int i = 0; i < urls.Length; i++)
        {
            if (urls[i].EndsWith(".meta"))
            {
                continue;
            }

            ChangeFormat(urls[i]);
        }
    }

    private void LoadWav(string[] urls)
    {
        for (int i = 0; i < urls.Length; i++)
        {
            if (urls[i].EndsWith(".meta"))
            {
                continue;
            }

            StartCoroutine(LoadWavMusic(urls[i]));
        }
    }

    /// <summary>
    /// 改变格式
    /// </summary>
    private void ChangeFormat(string mp3Path)
    {
        string[] str = mp3Path.Replace('\\', '/').Split('/');
        string clipName = str[str.Length - 1].Split('.')[0];
        string savepath = string.Format("{0}/Wav/{1}.wav", filePath, clipName);
        var stream = File.Open(mp3Path, FileMode.Open);
        var reader = new Mp3FileReader(stream);
        WaveFileWriter.CreateWaveFile(savepath, reader);
    }

    /// <summary>
    /// 加载wav
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator LoadWavMusic(string url)
    {
        string[] str = url.Replace('\\', '/').Split('/');
        string clipName = str[str.Length - 1].Split('.')[0];
        var www = new WWW(url);
        yield return www;
        var clip = www.GetAudioClip();
        clip.name = clipName;
        Debug.Log("GetAudioClip:" + clip.name);
        audioClips.Add(clip);
        if (!audioSource.isPlaying)
        {
            ChangeAudioClip();
        }
    }

    IEnumerator Load(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        AudioClip audio = www.GetAudioClip();
        if (audio == null) yield break;
        audio.name = url;
        audioClips.Add(audio);
        if (!audioSource.isPlaying)
        {
            ChangeAudioClip();
        }
    }

    public IEnumerator AudioEnd(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        ChangeAudioClip();
    }

    private void ChangeAudioClip()
    {
        index++;
        if (index >= audioClips.Count)
        {
            index = 0;
        }

        audioSource.clip = audioClips[index];
        audioSource.Play();
        StartCoroutine(AudioEnd(audioSource.clip));
    }
}


