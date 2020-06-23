using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;


/// <summary>
/// Appϵͳ�����ļ�
/// </summary>
public class AppConfig
{
    //��Ļ��ʾ�ֱ��ʼ���ʾģʽ
    public int ScreenWidth = 1920;
    public int ScreenHight = 1080;
    public FullScreenMode ScreenMode = FullScreenMode.FullScreenWindow;

    //�Ƿ��������
    public bool IsCursorVisible = false;

    //�Ƿ�������У��ģʽ
    public bool IsGeometricRectificationMode = true;

    //�Ƿ���ȫ���ޱ߿�ģʽ
    public bool IsNoBorderMode = false;

    //�Ƿ���Kinect
    public bool IsKinectOn = false;
}
    
/// <summary>
/// ����У�������ࣺ��������/����/����/�����ĸ�������꼰ƫ����
/// </summary>
public class PointData
{
    public List<Vector2> Points = new List<Vector2>();
    public List<Vector2> ChangeValues = new List<Vector2>();
}

/// <summary>
/// Ŀ��IP�Ͷ˿������ļ���Ŀǰ��Ҫ�ֶ���Ӽ��޸�(������Ҫ�������ļ�����ı���һ��)
/// </summary>
public class IPConfig
{
    public int LocalPort;
    public string RemoteIP;
    public int RemotePort;
}

/// <summary>
/// Kinect�����ļ��������������
/// </summary>
public class KinectConfig
{
    public float PlayerPosX;
    public float PlayerPosZ;
    public float PosRange;
}

/// <summary>
/// ���ݹ�����
/// </summary>
public class DataManager : Singleton<DataManager>
{
    private DataManager() { }

    public AppConfig AppConfig = new AppConfig();
    public PointData PointDatas = new PointData();
    public IPConfig IpConfig = new IPConfig();
    public KinectConfig KinectConfig = new KinectConfig();

    public AppConfig Init()
    {
        LoadLocalData();

        //AppConfig.SaveJson<AppConfig>(Application.streamingAssetsPath + "/AppConfig.json");
        return AppConfig;
    }

    public void LoadLocalData()
    {
        AppConfig = SerializeHelper.LoadJson<AppConfig>(Application.streamingAssetsPath + "/AppConfig.json");
        PointDatas = SerializeHelper.LoadJson<PointData>(Application.streamingAssetsPath + "/PointData.json");
        IpConfig = SerializeHelper.LoadJson<IPConfig>(Application.streamingAssetsPath + "/IPConfig.json");
        KinectConfig = SerializeHelper.LoadJson<KinectConfig>(Application.streamingAssetsPath + "/KinectConfig.json");

        //Udp��ʼ��
        if (IpConfig.IsNotNull())
        {
           UdpManager.Instance.Init(IpConfig);
        }     
    }

    public void SaveKinectConfig(KinectConfig config)
    {
        KinectConfig = config;
        SerializeHelper.SaveJson<KinectConfig>(config, Application.streamingAssetsPath + "/KinectConfig.json");
    }


}

