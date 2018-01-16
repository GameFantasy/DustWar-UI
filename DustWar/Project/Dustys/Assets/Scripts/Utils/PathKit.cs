using UnityEngine;
using System.Collections;
using System.IO;
//路径工具类
public class PathKit
{

    /** 后缀常量字符 */
    public const string SUFFIX = ".txt";
    const string PREFIX = "file://";
    const string FORMAT = ".dat";
    public static string RESROOT = Application.persistentDataPath + "/";

    public static string GetPath(string p_filename)
    {
        string _strPath = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            _strPath = Application.dataPath + "//" + p_filename + FORMAT;
        else if (Application.platform == RuntimePlatform.Android)
            _strPath = Application.persistentDataPath + "//" + p_filename + FORMAT;
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.IPhonePlayer)
            _strPath = Application.persistentDataPath + "//" + p_filename + FORMAT;

        return _strPath;
    }
}