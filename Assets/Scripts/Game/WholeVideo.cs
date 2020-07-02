using UnityEngine;
using QFramework;
using UnityEngine.Experimental.Video;
using UnityEngine.Video;
using System;
using Common;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class WholeVideo : ViewController
	{
	    private VideoPlayer mVideoPlayer;

        void Awake()
	    {
	        mVideoPlayer = GetComponent<VideoPlayer>();
        }

		void Start()
		{
            // Code Here		   
		    //Debug.Log("frameCount:"+mVideoPlayer.frameCount);
		    mVideoPlayer.loopPointReached += OnLoopPointReached;
            EventCenter.AddListener(EventType.TableVideoEnd,OnTableVideoEnd);
		}

	    void OnDestroy()
	    {
	        EventCenter.RemoveListener(EventType.TableVideoEnd, OnTableVideoEnd);
        }

        private void OnTableVideoEnd()
        {
            mVideoPlayer.Play();
        }

        private void OnLoopPointReached(VideoPlayer source)
        {
            Debug.Log(string.Format("Video:{0} PlayEnd", source.name));
            this.Delay(1f, ()=>
            {
                this.Hide();

                //通知交互端开始倒计时
                var msg = new UdpMessage(MessageDefine.WholeVideoEnd);
                UdpManager.Instance.SendMessage(msg.ToJson());
            });

            EventCenter.Broadcast(EventType.WholeVideoEnd);       
        }

	    public void Stop()
	    {
            mVideoPlayer.Stop();
	    }
    }
}
