using System.IO;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

/// <summary>
/// 与图片操作相关的工具类
/// </summary>
public class TextureUtils
{

    /// <summary>
    /// 将RenderTexture保存成一张png图片  
    /// </summary>
    /// <param name="rt">rt</param>
    /// <param name="contents">保存路径</param>
    /// <param name="pngName">图片名</param>
    /// <returns></returns>
    public static bool SaveRenderTextureToPNG(RenderTexture rt, string contents, string pngName)
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        byte[] bytes = png.EncodeToPNG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(png);
        png = null;
        RenderTexture.active = prev;
        return true;
    }

    /// <summary>
    /// 将RenderTexture保存成一张jpg图片  
    /// </summary>
    /// <param name="rt">rt</param>
    /// <param name="contents">保存路径</param>
    /// <param name="pngName">图片名</param>
    /// <returns></returns>
    public static bool SaveRenderTextureJPG(RenderTexture rt, string contents, string jpgName)
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D  jpg = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        jpg.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        byte[] bytes = jpg.EncodeToJPG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + jpgName + ".jpg", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(jpg);
        jpg = null;
        RenderTexture.active = prev;
        return true;

    }

    /// <summary>
    /// 将RenderTexture保存成一张png图片
    /// </summary>
    /// <param name="rt">rt</param>
    /// <param name="contents">保存路径</param>
    /// <param name="pngName">图片名</param>
    /// <param name="width">自定义rt宽</param>
    /// <param name="height">自定义rt高</param>
    /// <returns></returns>
    public static bool SaveRenderTextureToPNG(RenderTexture rt, string contents, string pngName,int width,int height)
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D png = new Texture2D(width, height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        byte[] bytes = png.EncodeToPNG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(png);
        png = null;
        RenderTexture.active = prev;
        return true;

    }

    /// <summary>
    /// 将Texture2D保存成一张png图片
    /// </summary>
    /// <param name="tex2d">tex2d</param>
    /// <param name="contents">保存路径</param>
    /// <param name="name">图片名</param>
    /// <returns></returns>
    public static bool SaveTexture2DToPNG(Texture2D tex2d, string contents, string name)
    {
        File.WriteAllBytes(contents + "/" + name + ".jpg", tex2d.EncodeToPNG());
        return true;
    }

    /// <summary>
    /// 将Texture2D保存成一张jpg图片
    /// </summary>
    /// <param name="tex2d">tex2d</param>
    /// <param name="contents">保存路径</param>
    /// <param name="name">图片名</param>
    /// <returns></returns>
    public static bool SaveTexture2DToJPG(Texture2D tex2d, string contents, string name)
    {
        File.WriteAllBytes(contents + "/" + name + ".jpg", tex2d.EncodeToJPG());
        return true;
    }

    /// <summary>
    /// 将一张彩色图转换成灰色图
    /// </summary>
    /// <param name="tex2d">tex2d</param>
    /// <returns></returns>
    public static Texture2D  ColorTexToGray(Texture2D tex2d)
    {
        Texture2D newTex2D = new Texture2D(tex2d.width, tex2d.height, TextureFormat.ARGB32, false);
        for (int i = 0; i < tex2d.height; i++)
        {
            for (int j = 0; j < tex2d.width; j++)
            {
                Color color = tex2d.GetPixel(j, i);
                float gray = (color.r + color.g + color.b) / 3;
                if (color.a > 0.5)
                {
                    newTex2D.SetPixel(j, i, new Color(gray, gray, gray));
                }
                else
                {
                    newTex2D.SetPixel(j, i, new Color(0, 0,0,0));
                }
            }
        }
        newTex2D.Apply();
        return newTex2D;
    }

    /// <summary>  
    /// 生成二维码  
    /// </summary>  
    public static Texture2D CreatQr(string QrCodeStr)
    {
        Texture2D t2d = new Texture2D(256, 256);
        //二维码写入图片    
        var color32 = Encode(QrCodeStr, t2d.width, t2d.height);
        t2d.SetPixels32(color32);
        t2d.Apply();
        return t2d;
    }

    /// <summary>
    /// 定义方法生成二维码 
    /// </summary>
    /// <param name="textForEncoding">需要生产二维码的字符串</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <returns></returns>       
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    /// <summary>
    /// 加载外部图片
    /// </summary>
    /// <param name="path">完整路径</param>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <returns></returns>
    public static Texture2D LoadImage(string path, int width, int height)
    {
        if (File.Exists(path))
        {
            byte[] imageBytes = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
            tex.LoadImage(imageBytes);
            return tex;
        }
        else
        {
            Debug.LogError("图片加载失败!");
        }

        return null;
    }
}
