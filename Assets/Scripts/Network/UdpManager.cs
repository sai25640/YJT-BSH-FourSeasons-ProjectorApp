using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using QFramework;
using UnityEngine;

public class UdpManager : MonoSingleton<UdpManager>, ISocket
{
    public delegate void OnGetReceive(string message);//接收到消息的委托
    public OnGetReceive onGetReceive;

    byte[] m_result = new byte[1024];

    Thread m_connectThread;

    UdpClient m_udpClient;
    IPEndPoint m_serverIPEndPoint;
    IPEndPoint m_recieveIPEndPoint;
    private Dictionary<string, IPEndPoint> m_IPEndPointDic;
    bool m_isConnected;

    private UdpManager() { }

    private Queue<string> m_receiveMsgs = new Queue<string>();
    //是否已连接的标识  
    public bool IsConnected()
    {
        return m_isConnected;
    }

    public void Connect(string ip, int port) { }

    //udp是无连接的，所以方法里只做了一些初始化操作
    public void Init(IPConfig ipConfig)
    {
        //将配置表中目标主机IP和端口存到字典中
        m_IPEndPointDic = new Dictionary<string, IPEndPoint>();
        m_IPEndPointDic.Add(SocketDefine.remote, new IPEndPoint(IPAddress.Parse(ipConfig.RemoteIP), ipConfig.RemotePort));

        //主程序端口
        m_udpClient = new UdpClient(ipConfig.LocalPort);
        m_isConnected = true;

        m_recieveIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        //开启一个线程连接
        m_connectThread = new Thread(new ThreadStart(RecieveMessage));
        m_connectThread.Start();

        //SendMessage("UdpManager Init Succeed!");
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="data">消息内容</param>
    public void SendMessage(string data)
    {
        if (IsConnected())
        {
            Debug.Log(string.Format("SendMsgTo[{0}] : {1}", m_IPEndPointDic[SocketDefine.remote].Address, data));

            byte[] bytes = Encoding.UTF8.GetBytes(data);

            m_udpClient.Send(bytes, bytes.Length, m_IPEndPointDic[SocketDefine.remote]);
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="data">消息内容</param>
    /// <param name="key">消息对象</param>
    public void SendMessage(string data, string key)
    {
        if (IsConnected())
        {
            Debug.Log(string.Format("SendMsgTo{0} : {1}",key,data));

            byte[] bytes = Encoding.UTF8.GetBytes(data);

            m_udpClient.Send(bytes, bytes.Length, m_IPEndPointDic[key]);
        }
    }

    public void Disconnect()
    {
        if (m_connectThread != null)
        {
            m_connectThread.Interrupt();
            //在调用此方法的线程上引发 ThreadAbortException，以开始终止此线程的过程。 调用此方法通常会终止线程。
            m_connectThread.Abort();
        }

        if (IsConnected())
        {
            Debug.Log("Disconnect");
            m_isConnected = false;

        }
        if (m_udpClient != null)
        {
            m_udpClient.Close();
        }
    }

    public void RecieveMessage()
    {
        while (IsConnected())
        {
            try
            {
                m_result = new byte[1024];
                //m_udpClient的port不是0的时候，会报错，无效参数
                m_result = m_udpClient.Receive(ref m_recieveIPEndPoint);
                string msg = Encoding.UTF8.GetString(m_result);
                //Debug.Log("RecieveMessage   " + msg);
                m_receiveMsgs.Enqueue(msg);
                if (onGetReceive != null)
                {
                    onGetReceive(msg);
                }
            }
            catch (Exception e)
            {
                Debug.Log("recieve error    " + e.Message);
                Disconnect();
            }
        }
    }

    void Update()
    {
        if (m_receiveMsgs.Count>0)
        {
            var msg = m_receiveMsgs.Dequeue();
            Debug.Log("ReceiveMsg:" + msg);
            var udp = msg.FromJson<UdpMessage>();
            //具体的消息处理
        }
    }

    protected override void OnDestroy()
    {
        Disconnect();
        base.OnDestroy();
    }
}
