using Common;
using UnityEngine;
using QFramework;
using UnityEngine.UI;
using UnityEngine.Video;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class LeftVideo : ViewController
	{
	    private VideoPlayer mVideoPlayer;
	    private RawImage mRawImage;
	    void Awake()
	    {
	        mVideoPlayer = GetComponent<VideoPlayer>();
	        mRawImage = GetComponent<RawImage>();
	    }

        void Start()
		{
            mVideoPlayer.prepareCompleted += OnPrepareCompleted;
		    mVideoPlayer.loopPointReached += OnLoopPointReached;
        }

	    void OnDestroy()
	    {
	        mVideoPlayer.prepareCompleted -= OnPrepareCompleted;
	        mVideoPlayer.loopPointReached -= OnLoopPointReached;
        }

        private void OnPrepareCompleted(VideoPlayer source)
        {
            //Debug.Log("OnPrepareCompleted");
            EventCenter.Broadcast(EventType.VideoPrepareCompleted, source);
        }

        private void OnLoopPointReached(VideoPlayer source)
        {
            PrepareFourSeasons();
        }

        public void PrepareFourSeasons()
	    {
	        string url = Application.streamingAssetsPath + "/Video/ËÄ¼¾-×ó.mp4";
            //Debug.Log(url);
            mVideoPlayer.url = url;
	        mVideoPlayer.isLooping = false;
            mVideoPlayer.targetTexture = new RenderTexture(1866, 800, 99);
            mRawImage.texture = mVideoPlayer.targetTexture;
            mVideoPlayer.Prepare();
        }

	    public void PrepareDragon()
	    {
            string url = Application.streamingAssetsPath + "/Video/Áú-Ç½Ãæ-×ó.mp4";
            //Debug.Log(url);
            mVideoPlayer.url = url;
            mVideoPlayer.isLooping = false;
            mVideoPlayer.targetTexture = new RenderTexture(1866,800,99);
	        mRawImage.texture = mVideoPlayer.targetTexture;
            mVideoPlayer.Prepare();
        }

	    public void Play()
	    {
            mVideoPlayer.Play();
	    }

	    public void Stop()
	    {
	        mVideoPlayer.Stop();
        }
        
    }


}
