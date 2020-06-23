using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Camera工具类
/// </summary>
public class CameraUtils : MonoSingleton<CameraUtils>
{
    private EventSystem eventSystem;
    private GraphicRaycaster graphicRaycaster;

    public override void OnSingletonInit()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        graphicRaycaster = UIManager.Instance.GetComponent<GraphicRaycaster>();
    }

    #region 射线检测
    /// <summary>
    /// 物理射线检测
    /// </summary>
    /// <param name="pos">触碰点</param>
    /// <param name="layerName">触摸层</param>
    /// <returns></returns>
    public bool PhysicsRaycaster(Vector2 pos, out RaycastHit hit, string layerName)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer(layerName)))
        {
            //Debug.Log("hit.point:" + hit.point);
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
            return true;
        }

        return false;
    }

    /// <summary>
    /// UI射线检测
    /// </summary>
    /// <param name="pos">触碰点</param>
    /// <returns></returns>
    private List<RaycastResult> GraphicRaycaster(Vector2 pos)
    {
        var data = new PointerEventData(eventSystem);
        data.position = pos;
        //Debug.Log(data.position);

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(data, results);

        return results;
    }

    /// <summary>
    /// 检测是否有触碰到UIBtn
    /// </summary>
    /// <param name="point">触碰点</param>
    /// <returns></returns>
    public bool IsTouchUIBtn(Vector2 point)
    {
        var res = GraphicRaycaster(point);
        for (int i = 0; i < res.Count; i++)
        {
            var btn = res[i].gameObject.GetComponent<Button>();
            if (btn != null)
            {
                //Debug.Log("Touch UIBtn: "+ res[i].gameObject.name);
                btn.onClick.Invoke();
                return true;
            }
        }
        return false;
    }


    #endregion

    #region Layer
    /// <summary>
    /// 显示层
    /// </summary>
    /// <param name="camera">摄像机</param>
    /// <param name="name">层名</param>
    public void ShowLayer(Camera camera, string name)
    {
        camera.cullingMask |= (1 << LayerMask.NameToLayer(name));
    }

    /// <summary>
    /// 隐藏层
    /// </summary>
    /// <param name="camera">摄像机</param>
    /// <param name="name">层名</param>
    public void HideLayer(Camera camera, string name)
    {
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer(name));
    }


    #endregion

    #region TempTex2D

    private Texture2D tempTex2D;
    private string tempTex2DFullPath;
    private string tempTex2DFilePath;

    /// <summary>
    /// 保存一张截图到本地
    /// </summary>
    /// <param name="name">图片名字</param>
    public void ScreenShotAndSaveOnLocal(string name)
    {
        tempTex2D = Camera.main.CaptureCamera(new Rect(0, 0, Screen.width, Screen.height));

        string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
        tempTex2DFilePath = Application.streamingAssetsPath + "/" + fileName;
        tempTex2DFullPath = tempTex2DFilePath + "/" + name + ".jpg";

        Directory.CreateDirectory(tempTex2DFilePath);
        if (Directory.Exists(tempTex2DFilePath))
        {
            TextureUtils.SaveTexture2DToJPG(tempTex2D, tempTex2DFilePath, name);
        }
        else
        {
            Debug.LogError(tempTex2DFilePath + " not exist!");
        }
    }

    public void ScreenShotAndSaveOnLocal(Camera camera, string name)
    {
        tempTex2D = camera.CaptureCamera(new Rect(0, 0, Screen.width, Screen.height));

        string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
        tempTex2DFilePath = Application.streamingAssetsPath + "/" + fileName;
        tempTex2DFullPath = tempTex2DFilePath + "/" + name + ".jpg";

        Directory.CreateDirectory(tempTex2DFilePath);
        if (Directory.Exists(tempTex2DFilePath))
        {
            TextureUtils.SaveTexture2DToJPG(tempTex2D, tempTex2DFilePath, name);
        }
        else
        {
            Debug.LogError(tempTex2DFilePath + " not exist!");
        }
    }

    public void ScreenShotAndSaveOnLocal(Rect rect, string name)
    {
        tempTex2D = Camera.main.CaptureCamera(rect);

        string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
        tempTex2DFilePath = Application.streamingAssetsPath + "/" + fileName;
        tempTex2DFullPath = tempTex2DFilePath + "/" + name + ".jpg";

        Directory.CreateDirectory(tempTex2DFilePath);
        if (Directory.Exists(tempTex2DFilePath))
        {
            TextureUtils.SaveTexture2DToJPG(tempTex2D, tempTex2DFilePath, name);
        }
        else
        {
            Debug.LogError(tempTex2DFilePath + " not exist!");
        }
    }

    public void ScreenShotAndSaveOnLocal(Camera camera, Rect rect, string name)
    {
        tempTex2D = camera.CaptureCamera(rect);

        string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
        tempTex2DFilePath = Application.streamingAssetsPath + "/" + fileName;
        tempTex2DFullPath = tempTex2DFilePath + "/" + name + ".jpg";

        Directory.CreateDirectory(tempTex2DFilePath);
        if (Directory.Exists(tempTex2DFilePath))
        {
            TextureUtils.SaveTexture2DToJPG(tempTex2D, tempTex2DFilePath, name);
        }
        else
        {
            Debug.LogError(tempTex2DFilePath + " not exist!");
        }
    }

    /// <summary>
    /// 获取缓存中的临时截图
    /// </summary>
    /// <returns></returns>
    public Texture2D GetTempTex2D()
    {
        if (tempTex2D.IsNotNull())
        {
            return tempTex2D;
        }
        else
        {
            Debug.LogError("TempTex2d is null!");
            return new Texture2D(Screen.width, Screen.height);
        }
    }

    /// <summary>
    ///  获取上次截图的完整路径
    /// </summary>
    /// <returns></returns>
    public string GetTempTex2DFullPath()
    {
        if (tempTex2DFullPath.IsNullOrEmpty())
        {         
            Debug.LogError("tempTex2DFullPath is empty!");
            return string.Empty;
        }
        else
        {
            return tempTex2DFullPath;
        }
    }

    /// <summary>
    ///  获取上次截图的文件路径(不包括文件名.jpg)
    /// </summary>
    /// <returns></returns>
    public string GetTempTex2DFilePath()
    {
        if (tempTex2DFilePath.IsNullOrEmpty())
        {
            Debug.LogError("tempTex2DFilePath is empty!");
            return string.Empty;
        }
        else
        {
            return tempTex2DFilePath;
        }
    }


    #endregion

}
