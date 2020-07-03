using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace FourSeasonsTest
{

    public class TestVideoPlayer : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();

            //可以通过url加载外部视频
            string url = Application.streamingAssetsPath + "/Video/龙-墙面.mp4"; 
            Debug.Log(url);
            if (url.Contains("龙"))
            {
                Debug.Log("有龙");
            }
            else
            {
                Debug.Log("没龙");
            }
            videoPlayer.url = url;

            videoPlayer.frameReady += OnFrameReady;
            videoPlayer.prepareCompleted += OnPrepareCompleted;

            //手动调用视频初始化，等OnPrepareCompleted函数回调后再播放视频
            videoPlayer.Prepare();
        }

        private void OnFrameReady(VideoPlayer source, long frameIdx)
        {
            Debug.Log("OnFrameReady:"+ frameIdx);
        }

        private void OnPrepareCompleted(VideoPlayer source)
        {
            Debug.Log("OnPrepareCompleted");
            videoPlayer.Play();
        }
    }
}
