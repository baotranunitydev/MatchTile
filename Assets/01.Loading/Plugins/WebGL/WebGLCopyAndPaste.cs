using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLCopyAndPasteAPI
{
    // Import các hàm từ JavaScript (WebGL)
    [DllImport("__Internal")]
    private static extern void initWebGLCopyAndPaste(StringCallback cutCopyCallback, StringCallback pasteCallback);

    [DllImport("__Internal")]
    private static extern bool passCopyToBrowser(string str);

    // Định nghĩa delegate cho các callback
    delegate void StringCallback(string content);

    // Hàm khởi tạo khi load game
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Init()
    {
        // Chỉ khởi tạo khi chạy trên WebGL và không phải ở chế độ Editor
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Thiết lập các callback để giao tiếp với WebGL
            initWebGLCopyAndPaste(GetClipboard, ReceivePaste);
        }
    }

    // Hàm xử lý sự kiện sao chép
    private static void SendKey(string baseKey)
    {
        // Xử lý sự kiện sao chép dữ liệu
    }

    // Callback từ mã không quản lý khi sao chép từ clipboard
    [AOT.MonoPInvokeCallback(typeof(StringCallback))]
    private static void GetClipboard(string key)
    {
        SendKey(key);
        // Do nothing here, since copying from browser to Unity clipboard is handled by JavaScript
    }

    // Callback từ mã không quản lý khi dán vào clipboard
    [AOT.MonoPInvokeCallback(typeof(StringCallback))]
    private static void ReceivePaste(string str)
    {
        // Xử lý sự kiện dán dữ liệu vào clipboard trong Unity
        GUIUtility.systemCopyBuffer = str;
    }

    // Phương thức public để gọi từ các lớp khác trong dự án Unity
    public static bool CopyToClipboard(string text)
    {
        // Gọi hàm passCopyToBrowser trong JavaScript từ WebGL
        bool success = passCopyToBrowser(text);
        if (!success)
        {
            Debug.LogError("Failed to copy text to clipboard.");
        }
        return success;
    }
}
