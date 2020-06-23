using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using QFramework;
using UniRx;
using UnityEngine;
using EventType = Common.EventType;

namespace MyFramework
{
    public class PlayerPosFilter : MonoSingleton<PlayerPosFilter>
    {
        private KinectManager kinectManager;
        private KinectConfig kinectConfig;
        long oldUserID;
        private int jointIndex = 0; //SpineBase

        public delegate void PlayerPosEventHandler(long userId);
        public event PlayerPosEventHandler PlayerNotInRect;
        public event PlayerPosEventHandler PlayerInRect;

        void Start()
        {
            kinectManager = KinectManager.Instance;
            kinectConfig = DataManager.Instance.KinectConfig;
        }

        void Update()
        {
            if (kinectManager && kinectManager.IsInitialized() && kinectManager.IsUserDetected())
            {
                // 检测站在指定位置的人是否离开指定区域
                if (kinectManager.GetAllUserIds().Contains(oldUserID))
                {
                    Vector3 SpineBasePos = kinectManager.GetJointKinectPosition(oldUserID, jointIndex);
                    if (!IsInRect(SpineBasePos))
                    {
                        //Debug.Log("**********不在指定区域");
                        if (PlayerNotInRect!=null)
                        {
                            PlayerNotInRect(oldUserID);
                        }                             
                    }
                }

                foreach (var userId in kinectManager.GetAllUserIds())
                {
                    // 追踪当前用户骨骼
                    if (kinectManager.IsJointTracked(userId, jointIndex))
                    {
                        Vector3 SpineBasePos = kinectManager.GetJointKinectPosition(userId, jointIndex);
                        if (IsInRect(SpineBasePos))
                        {
                            //var posX = SpineBasePos.x.ToString("F2");
                            //var posZ = SpineBasePos.z.ToString("F2");
                            //Debug.Log(string.Format("*********Pos: [{0}, {1}]" , posX,posZ));   
                            oldUserID = userId;
                            if (PlayerInRect!=null)
                            {
                                PlayerInRect(userId);
                            }             
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                foreach (var item in kinectManager.GetAllUserIds())
                {
                    if (kinectManager.IsJointTracked(item, jointIndex))
                    {
                        Vector3 SpineBasePos = kinectManager.GetJointKinectPosition(item, jointIndex);
                        kinectConfig.PlayerPosX = SpineBasePos.x;
                        kinectConfig.PlayerPosZ = SpineBasePos.z;
                        Debug.Log("PosX:" + kinectConfig.PlayerPosX);
                        Debug.Log("PosZ:" + kinectConfig.PlayerPosZ);
                        DataManager.Instance.SaveKinectConfig(kinectConfig);
                    }
                }
            }
        }

        bool IsInRect(Vector3 pos)
        {
            if ((kinectConfig.PlayerPosX - kinectConfig.PosRange) < pos.x &&
                pos.x < (kinectConfig.PlayerPosX + kinectConfig.PosRange) &&
                pos.z > (kinectConfig.PlayerPosZ - kinectConfig.PosRange) &&
                pos.z < (kinectConfig.PlayerPosZ + kinectConfig.PosRange))
            {
                return true;
            }

            return false;
        }

        #region 外部调用方法
        private IDisposable overTimeStream;
        /// <summary>
        /// 更新检测动作
        /// </summary>
        /// <param name="userId">对象id</param>
        public void UpdateDetectedPos(long userId)
        {
            //在这里添加需要识别的动作以及相关处理
            if (DetectedPose(userId))
            {
                if (overTimeStream != null)
                {
                    overTimeStream.Dispose();
                    overTimeStream = null;
                }
            }
            else
            {
                if (overTimeStream == null)
                {
                    overTimeStream = Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ => { });
                }
            }
        }

        private float timer = 0;
        private float overTime = 1f;
        private int step = 0;

        // 跑步或者走路动作
        private bool DetectedPose(long UserID)
        {
            if (kinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.KneeLeft) &&
                kinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.KneeRight))
            {
                switch (step)
                {
                    case 0:
                        //以左右膝盖Z值差做判断
                        if (kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z -
                            kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z >
                            0.15 ||
                            kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z -
                            kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z >
                            0.15)
                        {
                            step = 1;
                            //Debug.Log("step 0->1");                
                        }

                        break;
                    case 1:
                        {
                            //在规定时间内未完成动作判断失败
                            timer += Time.deltaTime;
                            if (timer >= overTime)
                            {
                                step = 0;
                                timer = 0;
                                //Debug.Log("time out!");
                                return false;
                            }

                            if (kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z -
                                kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z <
                                0.05 &&
                                kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z -
                                kinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z <
                                0.05)
                            {

                                step = 0;
                                timer = 0;
                                //Debug.Log("Runing");
                                return true;
                            }
                        }
                        break;
                }
            }

            return false;

        }
        #endregion

    }
}
