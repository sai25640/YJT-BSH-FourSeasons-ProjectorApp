using Common;
using UnityEngine;
using QFramework;
using UnityEngine.Video;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class LeftVideo : ViewController
	{
	    private VideoPlayer mVideoPlayer;

	    void Awake()
	    {
	        mVideoPlayer = GetComponent<VideoPlayer>();
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
	        mVideoPlayer.isLooping = true;
	        mVideoPlayer.Prepare();
        }

	    public void PrepareDragon()
	    {
            string url = Application.streamingAssetsPath + "/Video/Áú-Ç½Ãæ-×ó.mp4";
            //Debug.Log(url);
            mVideoPlayer.url = url;
            mVideoPlayer.isLooping = false;
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
