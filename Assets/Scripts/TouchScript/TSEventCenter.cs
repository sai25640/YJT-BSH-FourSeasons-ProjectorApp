using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class TSEventCenter : MonoBehaviour
{
    private MetaGesture mMetaGesture;
    void Awake()
    {
        mMetaGesture = GetComponent<MetaGesture>();
    }

    void Start()
    {
        mMetaGesture.PointerPressed += OnPointerPressed;
        mMetaGesture.PointerUpdated += OnPointerUpdated;
        mMetaGesture.PointerReleased += OnPointerReleased;
    }

    void OnDestroy()
    {
        mMetaGesture.PointerPressed -= OnPointerPressed;
        mMetaGesture.PointerUpdated -= OnPointerUpdated;
        mMetaGesture.PointerReleased -= OnPointerReleased;
    }

    private void OnPointerPressed(object sender, MetaGestureEventArgs e)
    {
        var pointer = e.Pointer;
        //Debug.Log(string.Format("PointerID:{0} PressedPoint{1}: ", pointer.Id, pointer.Position));
        CameraUtils.Instance.IsTouchUIBtn(pointer.Position); 
    }

    private void OnPointerUpdated(object sender, MetaGestureEventArgs e)
    {

    }

    private void OnPointerReleased(object sender, MetaGestureEventArgs e)
    {

    }
}
