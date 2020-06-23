using UnityEngine;
using System.Collections;
using MyFramework;

public class HandsOnHips : BasePose
{
    public HandsOnHips(KinectManager kinectManager) : base(kinectManager) { }

    public override void UpdateDetectedPos(long userId)
    {
        if (DetectedPose(userId))
        {
            Debug.Log("HandsOnHips");
        }
    }

    private float timer = 0;
    private float delayTime = 1f;
    protected override bool DetectedPose(long UserID)
    {
        if (mKinectManager.IsJointTracked(UserID, 5) && mKinectManager.IsJointTracked(UserID, 9) && mKinectManager.IsJointTracked(UserID, 1) && mKinectManager.IsJointTracked(UserID, 6) && mKinectManager.IsJointTracked(UserID, 10) && mKinectManager.IsJointTracked(UserID, 21) && mKinectManager.IsJointTracked(UserID, 23) && mKinectManager.IsJointTracked(UserID, 4) && mKinectManager.IsJointTracked(UserID, 8))
        {

            Vector3 joint5_4 = mKinectManager.GetJointKinectPosition(UserID, 4) - mKinectManager.GetJointKinectPosition(UserID, 5);
            Vector3 joint5_6 = mKinectManager.GetJointKinectPosition(UserID, 6) - mKinectManager.GetJointKinectPosition(UserID, 5);


            Vector3 joint9_8 = mKinectManager.GetJointKinectPosition(UserID, 8) - mKinectManager.GetJointKinectPosition(UserID, 9);
            Vector3 joint9_10 = mKinectManager.GetJointKinectPosition(UserID, 10) - mKinectManager.GetJointKinectPosition(UserID, 9);


            float ElbowLeftAngle = Vector3.Angle(joint5_4, joint5_6);
            float ElbowRightAngle = Vector3.Angle(joint9_8, joint9_10);


            if (mKinectManager.GetJointKinectPosition(UserID, 1).x - mKinectManager.GetJointKinectPosition(UserID, 5).x >= 0.2f && mKinectManager.GetJointKinectPosition(UserID, 9).x - mKinectManager.GetJointKinectPosition(UserID, 1).x >= 0.2f &&
                mKinectManager.GetJointKinectPosition(UserID, 5).y > mKinectManager.GetJointKinectPosition(UserID, 21).y && mKinectManager.GetJointKinectPosition(UserID, 9).y > mKinectManager.GetJointKinectPosition(UserID, 23).y &&
                 mKinectManager.GetJointKinectPosition(UserID, 5).x < mKinectManager.GetJointKinectPosition(UserID, 21).x && mKinectManager.GetJointKinectPosition(UserID, 9).x > mKinectManager.GetJointKinectPosition(UserID, 23).x && ElbowLeftAngle < 130f && ElbowRightAngle < 130f)
            {

                timer += Time.deltaTime;
                if (timer >= delayTime)
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
