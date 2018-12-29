using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Microsoft.Win32;

/// <summary>
/// 检查资源目录图片大小
/// </summary>
public static class AlasCheck
{
    /// <summary>
    /// 返回根目录下的所有结构目录
    /// </summary>
    /// <param name="rootPath">根节点</param>
    public static List<string> GetAllDir(string rootPath, List<string> list)
    {
        DirectoryInfo dir = new DirectoryInfo(rootPath);
        DirectoryInfo[] dirinfo = dir.GetDirectories();

        list.Add(rootPath);
        for (int i = 0; i < dirinfo.Length; i++)
        {
            list.Add(dirinfo[i].FullName);
            GetAllDir(dirinfo[i].FullName, list);
        }
        return list;
    }
    /// <summary>
    /// 搜索目录所有文件，输出不合格文件
    /// </summary>
    /// <param name="dirList">目录</param>
    /// <returns></returns>
    public static List<string> GetAllFilePath(List<string> dirList,string outputPath,string rootPath,float safeSize) {
        DirectoryInfo dir;
        FileInfo file;
        List<string> filePathList = new List<string>();
        for (int i = 0; i < dirList.Count; i++)
        {
            dir = new DirectoryInfo(dirList[i]);
            FileInfo[] info = dir.GetFiles();
            for (int j = 0; j < info.Length; j++) {
                if (!checkFileSize(info[j], safeSize)) {
                    filePathList.Add(info[j].FullName);
                    string sourceFile = info[j].FullName;
                    file = new FileInfo(sourceFile);

                    if (file.Exists)
                    {
                        string destinationFile = info[j].FullName.Replace(rootPath, outputPath);
                        int index = destinationFile.LastIndexOf("\\");
                        string destinationDir = destinationFile.Substring(0,index);
                        if (Directory.Exists(destinationDir))
                        {
                            file.CopyTo(destinationFile, true);
                            Debug.Log("复制文件" + info[j].Name + "成功");
                        }
                        else
                        {
                            Directory.CreateDirectory(destinationDir); //新建文件夹  
                            file.CopyTo(destinationFile, true);
                        }
                    }
                }
            }
        }
        return filePathList;
    }

    /// <summary>
    /// 根据文件大小判断文件是否合格
    /// </summary>
    /// <param name="info">文件数据</param>
    /// <param name="fileSize">文件大小(KB)</param>
    /// <returns></returns>
    public static bool checkFileSize(FileInfo info,float fileSize) {
        if (info.Length / 1024.0 >= fileSize)
            return false;
        return true;
    }

    /// <summary>
    /// 字符串转int
    /// </summary>
    /// <param name="intStr">要进行转换的字符串</param>
    /// <param name="defaultValue">默认值，默认：0</param>
    /// <returns></returns>
    public static int ParseInt(string intStr, int defaultValue = 0)
    {
        int parseInt;
        if (int.TryParse(intStr, out parseInt))
            return parseInt;
        return defaultValue;
    }

    public static string selectDir()

    {

        string bgImagePath = "";

        string path = "";

#if UNITY_EDITOR

        // Editor specific code here
        path = UnityEditor.EditorUtility.OpenFolderPanel("Load Images of Directory", UnityEngine.Application.dataPath, "*");

#endif

        //WWW ww = new WWW("file:///" + path);

        //print(ww.url);

        if (path != "")//load image as texture

        {

            //StartCoroutine(WaitLoad(path));

            Debug.Log("获得文件路径成功：" + path);

            bgImagePath = path;

        }

        return bgImagePath;

    }
}
