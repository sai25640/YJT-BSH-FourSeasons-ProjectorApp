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
		    EventCenter.AddListener(EventType.TableVideoEnd, OnTableVideoEnd);

		    StopAllVideoPlay();
		}

        void OnDestroy()
	    {
	        EventCenter.RemoveListener(EventType.StopPlayProjectVideo, StopAllVideoPlay);
	        EventCenter.RemoveListener(EventType.TableVideoEnd, OnTableVideoEnd);
        }

        private void StopAllVideoPlay()
        {
            //WholeVideo.Stop();
            FourSeasonVideo.Stop();
            MaskImage.Show();
        }

        private void OnTableVideoEnd()
        {
            FourSeasonVideo.PrepareDragon();
        }

    }
}
