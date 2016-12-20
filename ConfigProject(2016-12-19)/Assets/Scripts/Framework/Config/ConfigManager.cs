using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace XHConfig
{
    /// <summary>
    /// 配置表加载管理器
    /// 负责全局设置，数据加载，默认加载，异步加载实现
    /// </summary>
    public class ConfigManager
    {
        private static ConfigManager _instance;

        public static ConfigManager Instance
        {
            get { return _instance; }
        }

        static ConfigManager()
        {
            _instance = new ConfigManager();
        }

        protected ConfigManager()
        {
        }

        //是否开启预加载
        public static bool IsPreLoad = false;
        //配置文件资源路径
        public static string ResPath;
        //异步加载进度
        protected Action<int, int> Progress;
        //加载完成回调
        protected Action Finished;

        /// <summary>
        /// 默认加载的配置列表
        /// </summary>
        public List<Type> DefaultConfigs;
        /// <summary>
        /// 异步加载的配置列表
        /// </summary>
        public List<Type> AsyncConfigs;

        public void Init(Action<int, int> progress = null, Action finished = null)
        {
            LoadConfigs(DefaultConfigs, progress);
            if (IsPreLoad)
            {
                LoadConfigs(AsyncConfigs, progress, finished);
            }
            else
            {
                if (finished != null)
                {
                    finished();
                }
            }
        }

        /// <summary>
        /// 加载一组配置文件
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="progress"></param>
        public void LoadConfigs(List<Type> configs, Action<int, int> progress = null, Action finisned = null)
        {
            if (configs == null || configs.Count == 0)
            {
                if (finisned != null)
                {
                    finisned();
                }
                return;
            }
            int count = configs.Count;
            int index = 1;
            foreach (var item in configs)
            {
                LoadConfig(item);
                if (progress != null)
                {
                    progress(index, count);
                }
                index++;
            }
            if (finisned != null)
            {
                finisned();
            }
        }

        /// <summary>
        /// 加载一个配置文件
        /// </summary>
        /// <param name="item"></param>
        public void LoadConfig(Type item)
        {
            //获取配置表类的Config属性方法，调用其Get方法，触发加载
            var f = item.GetProperty("Config", ~BindingFlags.DeclaredOnly);
            if (f != null)
            {
                f.GetGetMethod().Invoke(null, null);
            }
        }

        /// <summary>
        /// 格式化指定类型的配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        public void FormatConfig<T>(out T config) where T : AbsConfig, new()
        {
            config = new T();
            string fileName = config.GetFileName<T>();
            AbsConfig.E_ConfigType configType = config.ConfigType;

            switch (configType)
            {
                case AbsConfig.E_ConfigType.XML:
                    config = FormatXMLConfig<T>(fileName);
                    break;
                case AbsConfig.E_ConfigType.JSON:
                    config = FormatJSONConfig<T>(fileName);
                    break;
                case AbsConfig.E_ConfigType.TXT:
                    config = FormatTxtConfig<T>(fileName);
                    break;
                default:
                    Debug.Log("Error: 没有指定的格式化方法，ConfigType不正确。");
                    break;
            }
            //反序列化没有调用构造函数，导致出来的Name等字段拿不到正确值
            //如果需要值，手动调用Init；需要修改访问权限
            //config.init();
        }

        /// <summary>
        /// 反序列化Txt文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        T FormatTxtConfig<T>(string fileName) where T : AbsConfig, new()
        {
            return TXTHelper.FormatConfig<T>(GetPath(fileName));
        }

        /// <summary>
        /// 反序列化XML文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        T FormatXMLConfig<T>(string fileName) where T : AbsConfig 
        {
            return XMLHelper.FormatConfig<T>(GetPath(fileName));
        }

        /// <summary>
        /// 反序列化JSON文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        T FormatJSONConfig<T>(string fileName) where T : AbsConfig
        {
            return JSONHelper.FormatConfig<T>(GetPath(fileName));
        }

        /// <summary>
        /// 配置文件完整路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected string GetPath(string fileName)
        {
            return ResPath + fileName;
        }
    }
}
