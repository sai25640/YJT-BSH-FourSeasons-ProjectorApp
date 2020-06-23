using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowsUtil
{
    #region 
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);


    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetParent(IntPtr hWnd);
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    public static extern void SetLastError(uint dwErrCode);

    private const uint SWP_SHOWWINDOW = 0x0040;
    private const int GWL_STYLE = -16;
    private const int WS_BORDER = 1;
    private const int WS_POPUP = 0x800000;


    public static IEnumerator Setposition(int x, int y, int with, int height)
    {
        yield return new WaitForSeconds(0.2f);
        SetWindowLong(GetProcessWnd(Process.GetCurrentProcess()), GWL_STYLE, WS_POPUP);
        bool result = SetWindowPos(GetProcessWnd(Process.GetCurrentProcess()), -1, x - 2, y - 2, with + 4, height + 4, SWP_SHOWWINDOW);
    }

    public static IntPtr GetProcessWnd(Process process)
    {
        IntPtr ptrWnd = IntPtr.Zero;
        uint pid = (uint)process.Id;  // ��ǰ���� ID


        bool bResult = EnumWindows(new WNDENUMPROC(delegate (IntPtr hwnd, uint lParam)
        {
            uint id = 0;


            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref id);
                if (id == lParam)    // �ҵ����̶�Ӧ�������ھ��
                {
                    ptrWnd = hwnd;   // �Ѿ����������
                    SetLastError(0);    // �����޴���
                    return false;   // ���� false ����ֹö�ٴ���
                }
            }


            return true;

        }), pid);


        return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : IntPtr.Zero;
    }
    #endregion

}
