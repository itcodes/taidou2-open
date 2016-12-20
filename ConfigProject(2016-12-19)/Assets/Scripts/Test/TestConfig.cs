using System;
using System.Collections.Generic;
using UnityEngine;
using XHConfig;

public class TestConfig : MonoBehaviour {

    void Awake()
    {
        ConfigManager.ResPath = Application.streamingAssetsPath + "/Data";
        ConfigManager.Instance.DefaultConfigs = new List<Type>() { typeof(ErrorCodeConfig), typeof(Error) };
        ConfigManager.Instance.Init(delegate (int a, int b) { Debug.Log("Progress: " + a + "     " + b); }, delegate () { Debug.Log("Finshed"); });
    }

    // Use this for initialization
    void Start () {
        string error = ErrorCodeConfig.GetErrorById(10000);

        Debug.Log(error);

        string error1 = Error.GetErrorById(10001);

        Debug.Log(error1);

        string name = JsonTest.Config.name;
        Debug.Log(name);

        GoodsConfigGoods goods = GoodsConfig.GetGoodsById(10000);
        if (goods != null)
        {
            Debug.Log(goods.Name);
        }

        StudentItem student = StudentConfig.GetStudent(12345);
        if (student != null)
        {
            Debug.Log(student.Name);
        }
        else
        {
            Debug.Log("没有找到数据");
        }
    }
}
