using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFramework
{
    
    public class PoseProcessCenter : MonoBehaviour
    {
        private KinectManager kinectManager;
        private PlayerPosFilter playerPosFilter;
        private List<BasePose> poseList = new List<BasePose>();
        void Start()
        {
            kinectManager = KinectManager.Instance;
            playerPosFilter = PlayerPosFilter.Instance;

            if (playerPosFilter)
            {
                InitEvent();
            }

            if (kinectManager)
            {
                AddNeededPose();
            }
        }

         void InitEvent()
        {
            playerPosFilter.PlayerNotInRect += OnPlayerNotInRect;
            playerPosFilter.PlayerInRect += OnPlayerInRect;
        }

        void OnDestroy()
        {
            playerPosFilter.PlayerNotInRect -= OnPlayerNotInRect;
            playerPosFilter.PlayerInRect -= OnPlayerInRect;

            poseList.Clear();
        }

        /// <summary>
        /// 添加需要处理的动作
        /// </summary>
        void AddNeededPose()
        {
            poseList.Clear();

            poseList.Add(new Walk(kinectManager));
            poseList.Add(new RaiseLeftHand(kinectManager));
            poseList.Add(new RaiseRightHand(kinectManager));
            poseList.Add(new HandsOnHips(kinectManager));
        }

        private void OnPlayerNotInRect(long userId)
        {
            Debug.Log(string.Format("UserID:{0} Lost!",userId));
        }

        private void OnPlayerInRect(long userId)
        {
            foreach (var pose in poseList)
            {
                pose.UpdateDetectedPos(userId);
            }
        }
    }
}
