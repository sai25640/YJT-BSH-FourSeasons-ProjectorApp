using UnityEngine;
using QFramework;
using Common;
using EventType = Common.EventType;
using System;
using UnityEngine.Video;

namespace FourSeasons
{
	public partial class FourSeasonVideo : ViewController
	{
        void Start()
        {
            // Code Here
            EventCenter.AddListener<VideoPlayer>(EventType.VideoPrepareCompleted, OnVideoPrepareCompleted);
        }

        void OnDestroy()
        {
            EventCenter.RemoveListener<VideoPlayer>(EventType.VideoPrepareCompleted, OnVideoPrepareCompleted);
        }

	    public void PrepareDragon()
	    {
	        this.Delay(1f, () => MaskImage.Hide());
            LeftVideo.gameObject.SetActive(true);
            MidVideo.gameObject.SetActive(true);
            RightVideo.gameObject.SetActive(true);
            LeftVideo.PrepareDragon();
            MidVideo.PrepareDragon();
            RightVideo.PrepareDragon();
        }

        public void Stop()
        {
            LeftVideo.Stop();
            MidVideo.Stop();
            RightVideo.Stop();
            LeftVideo.gameObject.SetActive(false);
            MidVideo.gameObject.SetActive(false);
            RightVideo.gameObject.SetActive(false);
        }

	    private int mPrepareCount = 0;
        private void OnVideoPrepareCompleted(VideoPlayer source)
        {
            Debug.Log(source.name+"Prepare Completed!");
            mPrepareCount++;
            //����������Ƶ��׼������Ժ�ͳһ��ʼ����
            if (mPrepareCount>=3)
            {
                LeftVideo.Play();
                MidVideo.Play();
                RightVideo.Play();
                mPrepareCount = 0;

                if (source.url.Contains("�ļ�"))
                {
                    //֪ͨ�����˿�ʼ����ʱ
                    var msg = new UdpMessage(MessageDefine.WholeVideoEnd);
                    UdpManager.Instance.SendMessage(msg.ToJson());
                }
            }
        }


    }
}
