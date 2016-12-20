
namespace XHConfig
{
    /// <summary>
    /// 错误提示的位置
    /// </summary>
    public enum E_ErrorLocal
    {
        None,
        Fly,
        MessageBox,
    }

    public partial class ErrorCodeConfig  : XmlConfig<ErrorCodeConfig>
    {
        /*
        /// <summary>
        /// 如果配置文件类名和文件名不同，重写Name
        /// 如果相对路径不同，重写Path
        /// </summary>
        protected override void Init()
        {
            Path = "/Xml/";
            Extension = ".xml";
            Name = "ErrorCodeConfig";
        }
        */

        public static E_ErrorLocal GetLocalById(int param)
        {
            //ErrorCodeConfigContent error = System.Array.Find<ErrorCodeConfigContent>(Config.Content, delegate (ErrorCodeConfigContent item)
            //{
            //    return item.Id == param;
            //});
            //if (null == error)
            //{
            //    return E_ErrorLocal.None;
            //}
            //return (E_ErrorLocal)error.Local;
            var ret = System.Array.Find<ErrorCodeConfigContent>(Config.Content, (item) => item.Id == param);
            if (ret != null)
            {
                return (E_ErrorLocal)ret.Local;
            }
            return E_ErrorLocal.None;
        }

        public static string GetErrorById(int param)
        {
            //ErrorCodeConfigContent error = System.Array.Find<ErrorCodeConfigContent>(Config.Content, delegate (ErrorCodeConfigContent item)
            //{
            //    return item.Id == param;
            //});
            //if (null == error)
            //{
            //    return "配置表中没有找到该错误信息！Error Id：" + param;
            //}
            //return error.Desc;

            var ret = System.Array.Find<ErrorCodeConfigContent>(Config.Content, (item) => item.Id == param);
            if (ret != null)
            {
                return ret.Desc;
            }
            return "Error: Not Find Error id:" + param;
        }
    }
}
