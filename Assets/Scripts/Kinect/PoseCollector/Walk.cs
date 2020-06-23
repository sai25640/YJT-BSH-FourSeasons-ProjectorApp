using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MyFramework
{
    public class Walk : BasePose
    {
        public Walk(KinectManager kinectManager) : base(kinectManager) { }
     
        private IDisposable overTimeStream;
        public override void UpdateDetectedPos(long userId)
        {
            if (DetectedPose(userId))
            {
                Debug.Log("Walking");
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
                    overTimeStream = Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ => {  });
                }
            }
        }

        private float timer = 0;
        private float overTime = 1f;
        private int step = 0;
        protected override bool DetectedPose(long UserID)
        {
            if (mKinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.KneeLeft) &&
               mKinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.KneeRight))
            {
                switch (step)
                {
                    case 0:
                        //以左右膝盖Z值差做判断
                        if (mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z -
                            mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z >
                            0.15 ||
                            mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z -
                            mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z >
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

                            if (mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z -
                                mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z <
                                0.05 &&
                                mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeRight).z -
                                mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.KneeLeft).z <
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
    }
}
