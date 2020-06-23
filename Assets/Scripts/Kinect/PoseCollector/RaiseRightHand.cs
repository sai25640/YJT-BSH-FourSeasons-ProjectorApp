using UnityEngine;
using System.Collections;

namespace MyFramework
{
    public class RaiseRightHand : BasePose
    {
        public RaiseRightHand(KinectManager kinectManager) : base(kinectManager) { }

        public override void UpdateDetectedPos(long userId)
        {
            if (DetectedPose(userId))
            {
                Debug.Log("Raising RightHand");             
            }       
        }

        private float timer = 0;
        private float delayTime = 0.5f;

        protected override bool DetectedPose(long UserID)
        {
            if (mKinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.HandTipRight) && mKinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.HandTipLeft) && mKinectManager.IsJointTracked(UserID, (int)KinectInterop.JointType.Neck))
            {

                if (mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.HandTipRight).y > mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.Neck).y &&
                    mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.HandTipRight).x > mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.Neck).x &&
                    mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.HandTipLeft).y < mKinectManager.GetJointKinectPosition(UserID, (int)KinectInterop.JointType.SpineMid).y)
                {

                    timer += Time.deltaTime;
                    if (timer >= 0.5f)
                    {            
                        timer = 0f;
                        return true;
                    }
                }
                else
                {
                    timer = 0f;
                }
            }
            return false;          
        }

    }
}
