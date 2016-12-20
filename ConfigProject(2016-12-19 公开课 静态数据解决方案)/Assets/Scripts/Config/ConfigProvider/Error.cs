
namespace XHConfig
{
    /// <summary>
    /// 错误提示的位置
    /// </summary>


    public partial class Error  : XmlConfig<Error>
    {
        
        /// <summary>
        /// 如果配置文件类名和文件名不同，重写Name
        /// 如果相对路径不同，重写Path
        /// </summary>
        protected override void Init()
        {
            base.Init();
            Info.Name = "ErrorConfig";
        }
        

        public static E_ErrorLocal GetLocalById(int id)
        {
            ErrorCodeConfigContent error = System.Array.Find<ErrorCodeConfigContent>(Config.Content, delegate (ErrorCodeConfigContent item)
            {
                return item.Id == id;
            });
            if (null == error)
            {
                return E_ErrorLocal.None;
            }
            return (E_ErrorLocal)error.Local;
        }

        public static string GetErrorById(int id)
        {
            ErrorCodeConfigContent error = System.Array.Find<ErrorCodeConfigContent>(Config.Content, delegate (ErrorCodeConfigContent item)
            {
                return item.Id == id;
            });
            if (null == error)
            {
                return "配置表中没有找到该错误信息！Error Id：" + id;
            }
            return error.Desc;
        }
    }
}
