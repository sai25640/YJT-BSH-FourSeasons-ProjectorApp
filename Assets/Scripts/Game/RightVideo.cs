using UnityEngine;
using QFramework;
using UnityEngine.Video;

namespace FourSeasons
{
	public partial class RightVideo : ViewController
	{
        private VideoPlayer mVideoPlayer;
        private VideoCanvas mVideoCanvas;
        private WholeVideo mWholeVideo;
        void Awake()
        {
            mVideoPlayer = GetComponent<VideoPlayer>();
            mVideoCanvas = transform.parent.GetComponent<VideoCanvas>();
            mWholeVideo = mVideoCanvas.WholeVideo;
        }

        void Start()
        {
            // Code Here
            mWholeVideo.VideoPlayer.loopPointReached += OnLoopPointReached;
        }

        private void OnLoopPointReached(VideoPlayer source)
        {
            mVideoPlayer.Play();
        }
    }
}
