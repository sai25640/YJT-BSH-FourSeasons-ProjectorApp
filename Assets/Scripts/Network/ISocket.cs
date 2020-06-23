using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISocket
{
    //是否已连接的标识
    bool IsConnected();

    //连接指定IP和端口的服务器
    void Connect(string ip, int port);

    //发送数据给服务器
    void SendMessage(string data);

    //断开连接
    void Disconnect();

    //接收服务器端Socket的消息
    void RecieveMessage();
}
