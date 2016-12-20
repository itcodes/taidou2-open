using System.Diagnostics;
using UnityEngine;

namespace XHConfig
{

    /// <summary>
    /// 配置表的基类
    /// 定义相关变量及加载方法
    /// </summary>
    public abstract class AbsConfig
    {
        /// <summary>
        /// 配置表类型
        /// </summary>
        public enum E_ConfigType
        {
            None,
            XML,
            JSON,
            TXT,
            Custom,
        }

        /// <summary>
        /// 配置表相关信息
        /// </summary>
        public class ConfigData
        {
            /// <summary>
            /// 配置表的文件名，主要是解决类名和文件名不同的情况
            /// </summary>
            public string Name;
            //配置文件的相对路径
            public string Path;
            //配置文件的扩展名
            public string Extension;
            /// <summary>
            /// 配置表类型
            /// </summary>
            public E_ConfigType ConfigType = E_ConfigType.None;

            /// <summary>
            /// 获取配置完整表相对路径
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public string GetPath<T>()
            {
                var type = typeof(T);
                //看是否有手动设置配置文件名，如果没有就使用类名作为文件名
                if (string.IsNullOrEmpty(Name))
                {
                    Name = type.Name;
                }
                return Path + Name + Extension;
            }
        }

        /// <summary>
        /// 配置表相关信息对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public ConfigData Info = new ConfigData();

        /// <summary>
        /// 获取指定配置文件的路径，拼接而成
        /// </summary>
        /// <typeparam name="T">配置类</typeparam>
        public string GetFileName<T>() 
        {
            return Info.GetPath<T>();
        }

        public E_ConfigType ConfigType
        {
            get {
                return Info.ConfigType;
            }
        }

        protected AbsConfig()
        {
            Init();
        }

        protected virtual void Init()
        {
            Info.Path = "/Xml/";
            Info.Extension = ".xml";
            Info.Name = GetType().Name;
            Info.ConfigType = E_ConfigType.XML;
        }

        protected static T GetConfig<T>() where T : AbsConfig, new()
        {
            T config;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ConfigManager.Instance.FormatConfig<T>(out config);
            sw.Stop();
            //加载该配置文件消耗的时间
            float loadTime = sw.ElapsedMilliseconds;
            UnityEngine.Debug.Log("Load Config FileName: " + config.Info.Name + " Times: " + loadTime + " MS");
            return config;
        }

    }


    /// <summary>
    /// 配置表的基类
    /// 提供了单例方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbsConfig<T> : AbsConfig where T : AbsConfig, new()
    {
        private static T config;

        public static T Config 
        {
            get
            {
                if (null == config)
                {
                    config = GetConfig<T>();
                    UnityEngine.Debug.Log("Load Config ClassType: " + typeof(T).ToString());
                }
                return config;
            }
        }
    }

    /// <summary>
    /// 设置XML配置表相关信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class XmlConfig<T> : AbsConfig<T> where T : AbsConfig, new()
    {
    }

    /// <summary>
    /// 设置Json配置表相关信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class JsonConfig<T> : AbsConfig<T> where T : AbsConfig, new()
    {
        protected override void Init()
        {
            Info.Path = "/Json/";
            Info.Extension = ".json";
            Info.Name = GetType().Name;
            Info.ConfigType = E_ConfigType.JSON;
        }
    }

    /// <summary>
    /// 设置自定义配置表相关信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TxtConfig<T> : AbsConfig<T> where T : AbsConfig, new()
    {
        protected override void Init()
        {
            Info.Path = "/Txt/";
            Info.Extension = ".bytes";
            Info.Name = GetType().Name;
            Info.ConfigType = E_ConfigType.TXT;
        }
    }

    /// <summary>
    /// 设置自定义配置表相关信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CustomConfig<T> : AbsConfig<T> where T : AbsConfig, new()
    {
        protected override void Init()
        {
            Info.Path = "/Custom/";
            Info.Extension = ".custom";
            Info.Name = GetType().Name;
            Info.ConfigType = E_ConfigType.Custom;
        }
    }
}
