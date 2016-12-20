using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XHConfig
{
    /// <summary>
    /// XML 工具摘要
    /// 负责XML序列化和反序列化
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// 反序列化XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T FormatConfig<T>(string path) where T : class
        {
            Stream stream = File.OpenRead(path);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            T res = (T)xs.Deserialize(stream);
            stream.Dispose();
            return res;
        }

        /// <summary>
        /// XML转换成byte[]
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static byte[] XmlDocToBytes(XmlDocument doc)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            StreamReader reader = new StreamReader(ms, System.Text.Encoding.UTF8);
            ms.Position = 0;
            string text = reader.ReadToEnd();
            reader.Close();
            ms.Close();
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(text);
        }

        /// <summary>
        /// 根据路径读取xml文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDoc(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            XmlDocument doc = BytesToXmlDoc(bytes);
            return doc;
        }

        /// <summary>
        /// byte[]转换成xmlDoc
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static XmlDocument BytesToXmlDoc(byte[] bytes)
        {
            XmlDocument doc = new XmlDocument();
            UTF8Encoding encoding = new UTF8Encoding();
            string text = encoding.GetString(bytes);
            doc.LoadXml(text);
            return doc;
        }
    }
}
