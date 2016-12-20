using System.IO;

namespace XHConfig
{
    /// <summary>
    /// Txt文档数据类必须实现的接口
    /// 解析Txt文档的字符串赋值给对应数据对象
    /// </summary>
    public interface IReader
    {
        void Reader(string content);
    }

    public class TXTHelper
    {
        /// <summary>
        /// 反序列化Txt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T FormatConfig<T>(string path) where T : class, new()
        {
            string str = File.ReadAllText(path);
            T data = new T();
            ((IReader)data).Reader(str);
            return data;
        }
    }
}
