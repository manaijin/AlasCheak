using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StartExe : MonoBehaviour
{
    [Tooltip("源文件根目录路径")]
    public string rootPath;
    [Tooltip("安全文件最大size(KB)")]
    public float safeSize;
    [Tooltip("输出文件根目录路径")]
    public string outputRootPath;

    private string _rootPath;
    private string _outputRootPath;

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoCheck() {
        try
        {
            rootPath = GameObject.Find("Canvas/Input_selectSource").GetComponent<InputField>().text;
            DeleteSpace(rootPath);
            rootPath = rootPath.Replace("/","\\");
            Debug.Log(rootPath);
            outputRootPath = GameObject.Find("Canvas/Input_selectTarget").GetComponent<InputField>().text;     
            DeleteSpace(outputRootPath);
            outputRootPath = outputRootPath.Replace("/", "\\");
            Debug.Log(outputRootPath);
            string str = GameObject.Find("Canvas/Input_safeFileSize").GetComponent<InputField>().text;
            DeleteSpace(str);
            safeSize = AlasCheck.ParseInt(str);
        }
        catch (InvalidCastException e) {
            Debug.Log("获取UI错误");
            return;
        }
       

        if (rootPath == null || rootPath == "")
        {
            Debug.Log("没有源文件根目录路径");
            return;
        }
        _rootPath = @rootPath;

        if (outputRootPath == null || outputRootPath == "")
        {
            Debug.Log("没有输出文件根目录路径");
            return;
        }
        _outputRootPath = @outputRootPath;

        List<string> allDir = new List<string>();
        allDir = AlasCheck.GetAllDir(_rootPath, allDir);
        List<string> allFilePath = AlasCheck.GetAllFilePath(allDir, _outputRootPath, _rootPath, safeSize);
        PrintList(allFilePath);
    }

    /// <summary>
    /// 输出List
    /// </summary>
    /// <param name="list"></param>
    private void PrintList(List<string> list) {
        string str = "";
        for (int i = 0; i < list.Count; i++) {
            str = string.Concat(str,list[i],"\n");
        }
        Debug.Log(str);
    }

    /// <summary>
    /// 删除字符串空格
    /// </summary>
    /// <param name="str"></param>
    private string DeleteSpace(string str) {        
        return str.Replace(" ", "");
    }

    
}
