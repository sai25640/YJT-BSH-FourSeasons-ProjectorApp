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
    public delegate void OnGetReceive(string message);//���յ���Ϣ��ί��
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
    //�Ƿ������ӵı�ʶ  
    public bool IsConnected()
    {
        return m_isConnected;
    }

    public void Connect(string ip, int port) { }

    //udp�������ӵģ����Է�����ֻ����һЩ��ʼ������
    public void Init(IPConfig ipConfig)
    {
        //�����ñ���Ŀ������IP�Ͷ˿ڴ浽�ֵ���
        m_IPEndPointDic = new Dictionary<string, IPEndPoint>();
        m_IPEndPointDic.Add(SocketDefine.remote, new IPEndPoint(IPAddress.Parse(ipConfig.RemoteIP), ipConfig.RemotePort));

        //������˿�
        m_udpClient = new UdpClient(ipConfig.LocalPort);
        m_isConnected = true;

        m_recieveIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        //����һ���߳�����
        m_connectThread = new Thread(new ThreadStart(RecieveMessage));
        m_connectThread.Start();

        //SendMessage("UdpManager Init Succeed!");
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    /// <param name="data">��Ϣ����</param>
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
    /// ������Ϣ
    /// </summary>
    /// <param name="data">��Ϣ����</param>
    /// <param name="key">��Ϣ����</param>
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
            //�ڵ��ô˷������߳������� ThreadAbortException���Կ�ʼ��ֹ���̵߳Ĺ��̡� ���ô˷���ͨ������ֹ�̡߳�
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
                //m_udpClient��port����0��ʱ�򣬻ᱨ����Ч����
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
            //�������Ϣ����
        }
    }

    protected override void OnDestroy()
    {
        Disconnect();
        base.OnDestroy();
    }
}
