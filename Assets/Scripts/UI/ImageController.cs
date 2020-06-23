using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageController : MonoBehaviour
{
    private List<PointInfo> infos = new List<PointInfo>();
    public PointInfo leftTopInfo;
    public PointInfo rightTopInfo;
    public PointInfo leftDownInfo;
    public PointInfo rightDownInfo;
    public UISkewImage SkewImage;

    public float offsetValue;
    private int index = -1;
    private PointInfo currentInfo;
    private bool isEditMode = false;

    GameObject point1;
    GameObject point2;
    GameObject point3;
    GameObject point4;

    public void Awake()
    {

        SkewImage = this.GetComponentInParent<UISkewImage>();

        point1 = GameObject.Instantiate(Resources.Load<GameObject>("Point"));
        point2 = GameObject.Instantiate(Resources.Load<GameObject>("Point"));
        point3 = GameObject.Instantiate(Resources.Load<GameObject>("Point"));
        point4 = GameObject.Instantiate(Resources.Load<GameObject>("Point"));
        point1.transform.SetParent(this.transform);
        point2.transform.SetParent(this.transform);
        point3.transform.SetParent(this.transform);
        point4.transform.SetParent(this.transform);



        leftTopInfo = point1.GetComponent<PointInfo>();
        rightTopInfo = point2.GetComponent<PointInfo>();
        leftDownInfo = point3.GetComponent<PointInfo>();
        rightDownInfo = point4.GetComponent<PointInfo>();

        leftTopInfo.PointName = "leftTopInfo";
        rightTopInfo.PointName = "rightTopInfo";
        leftDownInfo.PointName = "leftDownInfo";
        rightDownInfo.PointName = "rightDownInfo";

        leftTopInfo.myEvent.AddListener(ChangeLeftTopPos);
        rightTopInfo.myEvent.AddListener(ChangeRightTopPos);
        leftDownInfo.myEvent.AddListener(ChangeLeftDownPos);
        rightDownInfo.myEvent.AddListener(ChangeRightDownPos);

        //初始位置
        //Vector2 size = this.transform.ToRectTransform().rect.size;
        //leftTopInfo.transform.ToRectTransform().anchoredPosition = new Vector3(-(size.x / 2), size.y / 2, 0);
        //rightTopInfo.transform.ToRectTransform().anchoredPosition = new Vector3((size.x / 2), size.y / 2, 0);
        //leftDownInfo.transform.ToRectTransform().anchoredPosition = new Vector3(-(size.x / 2), -(size.y / 2), 0);
        //rightDownInfo.transform.ToRectTransform().anchoredPosition = new Vector3((size.x / 2), -(size.y / 2), 0);

        leftTopInfo.LoadPointPosition(DataManager.Instance.PointDatas.Points[0], DataManager.Instance.PointDatas.ChangeValues[0]);
        rightTopInfo.LoadPointPosition(DataManager.Instance.PointDatas.Points[1], DataManager.Instance.PointDatas.ChangeValues[1]);
        leftDownInfo.LoadPointPosition(DataManager.Instance.PointDatas.Points[2], DataManager.Instance.PointDatas.ChangeValues[2]);
        rightDownInfo.LoadPointPosition(DataManager.Instance.PointDatas.Points[3], DataManager.Instance.PointDatas.ChangeValues[3]);

        this.infos.Add(leftTopInfo);
        this.infos.Add(rightTopInfo);
        this.infos.Add(leftDownInfo);
        this.infos.Add(rightDownInfo);
        ChangeCurrentInfo();

  
        point1.SetActive(isEditMode);
        point2.SetActive(isEditMode);
        point3.SetActive(isEditMode);
        point4.SetActive(isEditMode);


    }

    #region EventCallBack   更改image顶点坐标位置

    private void ChangeRightDownPos(Vector3 arg0)
    {
        SkewImage.OffsetRightButtom = arg0;
    }

    private void ChangeLeftDownPos(Vector3 arg0)
    {
        SkewImage.OffsetLeftButtom = arg0;
    }

    private void ChangeRightTopPos(Vector3 arg0)
    {
        SkewImage.OffsetRightTop = arg0;
    }

    private void ChangeLeftTopPos(Vector3 value)
    {
        SkewImage.OffsetLeftTop = value;

    }


    #endregion


    float speed = 1;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            isEditMode = !isEditMode;
            point1.SetActive(isEditMode);
            point2.SetActive(isEditMode);
            point3.SetActive(isEditMode);
            point4.SetActive(isEditMode);

            if (!isEditMode)
            {
                DataManager.Instance.PointDatas.Points[0] = leftTopInfo.GetPointPos();
                DataManager.Instance.PointDatas.Points[1] = rightTopInfo.GetPointPos();
                DataManager.Instance.PointDatas.Points[2] = leftDownInfo.GetPointPos();
                DataManager.Instance.PointDatas.Points[3] = rightDownInfo.GetPointPos();

                DataManager.Instance.PointDatas.ChangeValues[0] = leftTopInfo.GetChangeValue();
                DataManager.Instance.PointDatas.ChangeValues[1] = rightTopInfo.GetChangeValue();
                DataManager.Instance.PointDatas.ChangeValues[2] = leftDownInfo.GetChangeValue();
                DataManager.Instance.PointDatas.ChangeValues[3] = rightDownInfo.GetChangeValue();

                DataManager.Instance.PointDatas.SaveJson<PointData>(Application.streamingAssetsPath + "/PointData.json");
            }

        }


        if (!isEditMode) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCurrentInfo();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 2;
        }

        //一下一下
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentInfo.ChangeX(-offsetValue*speed);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentInfo.ChangeX(+offsetValue * speed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentInfo.ChangeY(+offsetValue * speed);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentInfo.ChangeY(-offsetValue * speed);
        }

        //按住不放
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentInfo.ChangeX(-offsetValue * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentInfo.ChangeX(+offsetValue * speed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentInfo.ChangeY(+offsetValue * speed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentInfo.ChangeY(-offsetValue * speed);
        }
    }

    private void ChangeCurrentInfo()
    {
        index++;
        if (index >= infos.Count)
        {
            index = 0;
        }
        //Debug.Log(index);
        currentInfo = infos[index];
        currentInfo.SelectPoint();
        for (int i = 0; i < infos.Count; i++)
        {
            if (i != index)
            {
                infos[i].NoSelectPoint();
            }
        }
    }

    public void OnDestory()
    {
        for (int i = 0; i < infos.Count; i++)
        {
            Destroy(infos[i]);
        }
    }
}
