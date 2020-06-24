using UnityEngine;
using QFramework;
using UnityEngine.Experimental.Video;
using UnityEngine.Video;
using System;

namespace FourSeasons
{
	public partial class WholeVideo : ViewController
	{
	    private VideoPlayer mVideoPlayer;
        public VideoPlayer VideoPlayer
        {
            get => mVideoPlayer;
        }

        void Awake()
	    {
	        mVideoPlayer = GetComponent<VideoPlayer>();
        }

		void Start()
		{
            // Code Here		   
		    //Debug.Log("frameCount:"+mVideoPlayer.frameCount);
		    mVideoPlayer.frameReady += OnFrameReady;
		    mVideoPlayer.loopPointReached += OnLoopPointReached;
		}

        private void OnFrameReady(VideoPlayer source, long frameIdx)
        {
            Debug.Log(string.Format("Video:{0} OnFrameReady ByFrameIdx:{1}", source.name,frameIdx));
        }

        private void OnLoopPointReached(VideoPlayer source)
        {
            Debug.Log(string.Format("Video:{0} PlayEnd", source.name));
            this.Delay(1f, ()=>this.Hide());
        }
     
    }
}
