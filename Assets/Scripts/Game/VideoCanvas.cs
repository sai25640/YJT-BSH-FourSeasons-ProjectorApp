using Common;
using UnityEngine;
using QFramework;
using UniRx;
using EventType = Common.EventType;
using System;

namespace FourSeasons
{
	public partial class VideoCanvas : ViewController
	{
		void Start()
		{
		    // Code Here
            EventCenter.AddListener(EventType.StopPlayProjectVideo,StopAllVideoPlay);
        }

        void OnDestroy()
	    {
	        EventCenter.RemoveListener(EventType.StopPlayProjectVideo, StopAllVideoPlay);
        }

        private void StopAllVideoPlay()
        {
            WholeVideo.Stop();
            LeftVideo.Stop();
            MidVideo.Stop();
            RightVideo.Stop();
            WholeVideo.Show();
        }

    }
}
