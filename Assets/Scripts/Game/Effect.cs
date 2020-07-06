using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class Effect : MonoBehaviour
{
    private RawImage mRawImage;
    private VideoPlayer mVideoPlayer;

    void Awake()
    {
        mRawImage = GetComponent<RawImage>();
        mVideoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        mVideoPlayer.targetTexture = new RenderTexture(1500,1600,10);
        mRawImage.texture = mVideoPlayer.targetTexture;
        mVideoPlayer.loopPointReached += OnLoopPointReached;
    }

    void OnDestroy()
    {
        mVideoPlayer.loopPointReached -= OnLoopPointReached;
    }

    public void Init(Transform parent)
    {
        this.Awake();
        this.Start();

        transform.parent = parent;
        transform.localPosition = new Vector3(Random.Range(-850,850),-400,0);
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = new Vector3(1,1,-1);

        Play();
    }

    public void Play()
    {
        if (mVideoPlayer.targetTexture!=null && mRawImage.texture!=null)
        {
            mVideoPlayer.isLooping = false;
            mVideoPlayer.Play();
        }
    }

    private void OnLoopPointReached(VideoPlayer source)
    {
        Destroy(this.gameObject);
    }
}
