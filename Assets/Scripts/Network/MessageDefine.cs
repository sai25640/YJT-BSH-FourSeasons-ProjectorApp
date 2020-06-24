//Udp传递的Json消息类在这里定义

using System;
using UnityEngine;

#region UdpMessage

public class UdpMessage
{
    public string Msg;
    public string Data;

    public UdpMessage(string msg)
    {
        Msg = msg;
        Data = string.Empty;
    }

    public UdpMessage(string msg, string data)
    {
        Msg = msg;
        Data = data;
    }
}

public static class UdpMessageExtension
{
    public static string ToJson(this UdpMessage udp)
    {
        return JsonUtility.ToJson(udp);
    }

    public static UdpMessage FromJson(this UdpMessage udp, string json)
    {
        return JsonUtility.FromJson<UdpMessage>(json);
    }
}


#endregion

#region MessageDefine
public static class MessageDefine
{
    public const string TableVideoEnd = "TableVideoEnd";
    public const string StopPlayProjectVideo = "StopPlayProjectVideo";
}
#endregion

