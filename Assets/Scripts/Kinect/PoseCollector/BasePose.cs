using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFramework
{
    /// <summary>
    /// 所有Kinect动作的基类
    /// </summary>
    public class BasePose
    {
        protected KinectManager mKinectManager;
        public  BasePose(KinectManager kinectManager)
        {
            mKinectManager = kinectManager;
        }

        /// <summary>
        /// 在PoseProcessCenter类中调用
        /// </summary>
        /// <param name="userId">用户id</param>
        public virtual void UpdateDetectedPos(long userId)
        {
            if (DetectedPose(userId))
            {
                //如果检测到动作的处理
                //在子类中处理
            }
            else
            {
                //没检测到动作的处理
                //在子类中处理
            }
        }

        /// <summary>
        /// 具体的检测方法
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        protected virtual bool DetectedPose(long userId)
        {
            //在子类中实现
            return false;
        }

    }
}
