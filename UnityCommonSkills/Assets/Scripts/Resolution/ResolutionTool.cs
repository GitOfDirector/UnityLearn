using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResolutionTool : MonoBehaviour
{

    #region GetDeviceCaps常量

    const int HORZSIZE = 4;                            //物理屏幕的宽度（毫米）
    const int VERTSIZE = 6;                             //物理屏幕的高度（毫米）
    const int HORZRES = 8;                             //屏幕的宽度（像素）
    const int VERTRES = 10;                            //屏幕的高度（光栅线）
    const int LOGPIXELSX = 88;                      //沿屏幕宽度每逻辑英寸的像素数，在多显示器系统中，该值对所显示器相同
    const int LOGPIXELSY = 90;                      //沿屏幕高度每逻辑英寸的像素数，在多显示器系统中，该值对所显示器相同
    const int DESKTOPVERTRES = 117;
    const int DESKTOPHORZRES = 118;         //Windows NT：可视桌面的以像素为单位的宽度。如果设备支持一个可视桌面或双重显示则此值可能大于VERTRES

    #endregion

    #region GetSystemMetrics常量

    const int SM_CXSCREEN = 0; //屏幕宽度
    const int SM_CYSCREEN = 1; //屏幕高度

    #endregion

    #region 属性

    /// <summary>
    /// 获取屏幕分辨率---逻辑分辨率，经缩放之前
    /// </summary>
    public static Vector2 WorkingArea
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Vector2 size = new Vector2();
            size.x = GetDeviceCaps(hdc, HORZRES);
            size.y = GetDeviceCaps(hdc, VERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }
    }

    /// <summary>
    /// 获取真实设置的桌面分辨率大小---经缩放之后
    /// </summary>
    public static Vector2 DESKTOP
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Vector2 size = new Vector2();
            size.x = GetDeviceCaps(hdc, DESKTOPHORZRES);
            size.y = GetDeviceCaps(hdc, DESKTOPVERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }
    }

    /// <summary>
    /// 当前系统DPI_X 大小 一般为96
    /// </summary>
    public static int DpiX
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
            ReleaseDC(IntPtr.Zero, hdc);
            return DpiX;
        }
    }

    /// <summary>
    /// 当前系统DPI_Y 大小 一般为96
    /// </summary>
    public static int DpiY
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int DpiX = GetDeviceCaps(hdc, LOGPIXELSY);
            ReleaseDC(IntPtr.Zero, hdc);
            return DpiX;
        }
    }

    /// <summary>
    /// 获取宽度缩放百分比
    /// </summary>
    public static float ScaleX
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int t = GetDeviceCaps(hdc, DESKTOPHORZRES);
            int d = GetDeviceCaps(hdc, HORZRES);
            float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return ScaleX;
        }
    }

    /// <summary>
    /// 获取高度缩放百分比
    /// </summary>
    public static float ScaleY
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return ScaleY;
        }
    }

    /// <summary>
    /// 屏幕的真实尺寸---毫米为单位
    /// </summary>
    public static Vector2 ScreenPhysicalSize
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Vector2 size = new Vector2();
            size.x = GetDeviceCaps(hdc, HORZSIZE);
            size.y = GetDeviceCaps(hdc, VERTSIZE);
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }
    }

    #endregion

    #region Win32 API

    /// <summary>
    /// 获取指定设备的性能参数该方法将所取得的硬件设备信息保存到一个D3DCAPS9结构中
    /// 在多监视器系统上，如果hdc是桌面，GetDeviceCaps将返回主监视器的功能。
    /// 如果您想要其他监视器的信息，则必须使用多监视器API或CreateDC来获取特定监视器的设备上下文（DC）的HDC。
    /// </summary>
    /// <param name="hdc">要查询其设备的信息的设备场景</param>
    /// <param name="nIndex">根据GetDeviceCaps索引表所示常数确定返回信息的类型</param>
    /// <returns>设备相关信息的尺寸大小</returns>
    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    /// <summary>
    /// 根据指定的等级类型检索指定窗口普通的、典型的或特有的设备上下文环境
    /// </summary>
    /// <param name="ptr">设备上下文环境被检索的窗口的句柄，如果该值为NULL，GetDC则检索整个屏幕的设备上下文环境</param>
    /// <returns>如果成功，返回指定窗口客户区的设备上下文环境；如果失败，返回值为Null</returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr ptr);

    /// <summary>
    /// 释放设备上下文环境（DC）供其他应用程序使用
    /// </summary>
    /// <param name="hWnd">指向要释放的设备上下文环境所在的窗口的句柄</param>
    /// <param name="hDc">指向要释放的设备上下文环境的句柄</param>
    /// <returns>返回值说明了设备上下文环境是否释放；如果释放成功，则返回值为1；如果没有释放成功，则返回值为0</returns>
    [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
    public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

    /// <summary>
    /// 通过设置不同的标识符就可以获取系统分辨率、窗体显示区域的宽度和高度、
    /// 滚动条的宽度和高度
    /// </summary>
    /// <param name="nIndex"></param>
    /// <returns></returns>
    [DllImport("user32")]
    public static extern int GetSystemMetrics(int nIndex);

    #endregion

    void Start()
    {
        //当前显示器---DELL U2212HM
        //21.7英寸 48厘米 * 27厘米 16 : 9
        //官方数据：21.5英寸

        //1英寸 = 2.54厘米

        Debug.Log(GetSystemMetrics(SM_CXSCREEN));//1920---当前显示分辨率宽 / 自定义缩放比例
        Debug.Log(GetSystemMetrics(SM_CYSCREEN));//1080---当前显示分辨率高 / 自定义缩放比例

        Debug.Log(WorkingArea);//(1536.0, 864.0)
        Debug.Log(DpiX);//96
        Debug.Log(DpiY);//96
        Debug.Log(DESKTOP);//(1920.0, 1080.0)
        Debug.Log(ScaleX);//1.25
        Debug.Log(ScaleY);//1.25
        Debug.Log(ScreenPhysicalSize.x);//475mm---18.7inch
        Debug.Log(ScreenPhysicalSize.y);//267mm---10.5inch

        Debug.Log(Screen.currentResolution);//1920 x 1080 @ 60Hz---系统推荐分辨率（更改显示并不影响输出结果）
        Debug.Log(Screen.width);//1024---Game窗口有效宽
        Debug.Log(Screen.height);//768---Game窗口有效高
        Debug.Log(Screen.dpi);//96
        Debug.Log(Camera.main.pixelRect);


    }

    void Update()
    {
        //Debug.Log(Input.mousePosition);
    }



}
