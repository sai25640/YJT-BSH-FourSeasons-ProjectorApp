using UnityEngine;
using QFramework;
using Common;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class FourSeasonVideo : ViewController
	{
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
            //Debug.Log("FourSeasons Play");
            LeftVideo.Play();
            MidVideo.Play();
            RightVideo.Play();
        }

        public void Stop()
        {
            LeftVideo.Stop();
            MidVideo.Stop();
            RightVideo.Stop();
        }
    }
}
