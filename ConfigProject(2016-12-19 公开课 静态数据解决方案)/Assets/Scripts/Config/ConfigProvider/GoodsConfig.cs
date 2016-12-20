namespace XHConfig
{
    /// <summary>
    /// 物品配置表，测试用
    /// </summary>
    public partial class GoodsConfig : XmlConfig<GoodsConfig>
    {
        /// <summary>
        /// 如果xml文件不在Xml目录下，重新赋值Path为该xml当前相对目录；默认xml在Xml目录下
        /// 如果xml文件名字和类名不同，重新赋值Name为该xml的文件名；默认文件名和类名一样
        /// 如果xml文件后缀名不是.xml，重新赋值Extension为该xml的扩展名；默认xml后缀名为.xml
        /// 配置表类型ConfigType决定了该文件的反序列化方式
        /// </summary>
        protected override void Init()
        {
            base.Init();
            Info.Path = "/OtherXml/";
        }

        /// <summary>
        /// 根据ID获取物品配置表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static GoodsConfigGoods GetGoodsById(int param)
        {
            return System.Array.Find<GoodsConfigGoods>(Config.Goods, (item)=>item.Id == param);
        }
    }
}
