using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PointInfo : MonoBehaviour
{
    public MyEvent myEvent = new MyEvent();

    public class  MyEvent:UnityEvent<Vector3>
    {
        
    }

    private float z=0;
    private float x;
    private string pointName;
    public string PointName
    {
        get
        {
            return pointName;
        }

        set
        {
            pointName = value;
        }
    }

    public void ChangeX(float offset)
    {
        Debug.Log("ChangeX : " + offset);
        this.x += offset;
        Vector2 v2 = this.transform.ToRectTransform().anchoredPosition;
        this.transform.ToRectTransform().anchoredPosition = new Vector2(v2.x + offset, v2.y);
        myEvent.Invoke(new Vector3(x,y,z));
    }

    private float y;

    public void ChangeY(float offset)
    {
        Debug.Log("ChangeY : " + offset);
        this.y += offset;
        Vector2 v2 = this.transform.ToRectTransform().anchoredPosition;
        this.transform.ToRectTransform().anchoredPosition = new Vector2(v2.x , v2.y + offset);
        myEvent.Invoke(new Vector3(x, y, z));
    }

    private Image img;

    void Awake()
    {
        img = this.GetComponent<Image>();
    }

    public void  Start()
    {
        transform.localScale = Vector3.one;
    }

    public Vector2 GetPointPos()
    {
        return this.transform.ToRectTransform().anchoredPosition;
    }

    public Vector2 GetChangeValue()
    {
        return new Vector2(x, y);
    }

    public void SelectPoint()
    {
        img.color = Color.red;
    }

    public void NoSelectPoint()
    {
        img.color = Color.green;
    }

    public void LoadPointPosition(Vector2 vec,Vector2 changeValue)
    {
        this.transform.ToRectTransform().anchoredPosition = new Vector2(vec.x, vec.y);
        myEvent.Invoke(new Vector3(changeValue.x, changeValue.y, z));

        LogPointPosition();
    }

    public void SavePointPosition(Vector2 vec)
    {
        vec = this.transform.ToRectTransform().anchoredPosition;
    }

    public void LogPointPosition()
    {
        var pos =this.transform.ToRectTransform().anchoredPosition;
        Debug.Log(string.Format(pointName + "X : {0}  " + pointName + "Y : {1}", pos.x, pos.y));
    }
}

public static class UnityExtension
{
    public static RectTransform ToRectTransform(this Transform ts)
    {
        return  ts as RectTransform;
    }

}

