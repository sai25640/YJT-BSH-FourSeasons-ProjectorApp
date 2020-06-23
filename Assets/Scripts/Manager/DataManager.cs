using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;


/// <summary>
/// App系统配置文件
/// </summary>
public class AppConfig
{
    //屏幕显示分辨率及显示模式
    public int ScreenWidth = 1920;
    public int ScreenHight = 1080;
    public FullScreenMode ScreenMode = FullScreenMode.FullScreenWindow;

    //是否隐藏鼠标
    public bool IsCursorVisible = false;

    //是否开启几何校正模式
    public bool IsGeometricRectificationMode = true;

    //是否开启全屏无边框模式
    public bool IsNoBorderMode = false;

    //是否开启Kinect
    public bool IsKinectOn = false;
}
    
/// <summary>
/// 几何校正数据类：保存左上/右上/左下/右下四个点的坐标及偏移量
/// </summary>
public class PointData
{
    public List<Vector2> Points = new List<Vector2>();
    public List<Vector2> ChangeValues = new List<Vector2>();
}

/// <summary>
/// 目标IP和端口配置文件，目前需要手动添加及修改(变量名要与配置文件定义的保持一致)
/// </summary>
public class IPConfig
{
    public int LocalPort;
    public string RemoteIP;
    public int RemotePort;
}

/// <summary>
/// Kinect配置文件，玩家区域设置
/// </summary>
public class KinectConfig
{
    public float PlayerPosX;
    public float PlayerPosZ;
    public float PosRange;
}

/// <summary>
/// 数据管理类
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

        //Udp初始化
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

