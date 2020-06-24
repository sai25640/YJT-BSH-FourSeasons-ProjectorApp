using Common;
using UnityEngine;
using QFramework;
using UnityEngine.Video;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class RightVideo : ViewController
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
