using UnityEngine;
using QFramework;
using System;
using UnityEngine.Video;
using Common;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class MidVideo : ViewController
	{
        private VideoPlayer mVideoPlayer;
        void Awake()
        {
            mVideoPlayer = GetComponent<VideoPlayer>();
        }

        void Start()
        {
            // Code Here
            EventCenter.AddListener(EventType.WholeVideoEnd, OnWholeVideoEnd);
        }

        void OnDestroy()
        {
            EventCenter.RemoveListener(EventType.WholeVideoEnd, OnWholeVideoEnd);
        }

        private void OnWholeVideoEnd()
        {
            mVideoPlayer.Play();
        }
    }
}
