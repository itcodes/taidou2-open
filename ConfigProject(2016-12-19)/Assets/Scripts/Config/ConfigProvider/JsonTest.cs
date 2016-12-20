namespace XHConfig
{
    public partial class JsonTest : JsonConfig<JsonTest>
    {
        protected override void Init()
        {
            base.Init();
            Info.Name = "abc";
        }

        public static Link GetLink(string param)
        {
            return Config.links.Find((i) => i.name == param);
        }
    }
}