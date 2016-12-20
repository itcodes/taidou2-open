namespace XHConfig
{
    public partial class StudentConfig : TxtConfig<StudentConfig>
    {
        protected override void Init()
        {
            base.Init();
            Info.Name = "TestTxt";
        }
        //public static StudentItem GetStudent(int id)
        //{
        //    StudentItem student = System.Array.Find<StudentItem>(Config.Items, delegate (StudentItem item)
        //    {
        //        return item.ID == id;
        //    });
        //    return student;
        //}

        public static StudentItem GetStudent(int id)
        {
            return System.Array.Find<StudentItem>(Config.Items, (a) => a.ID == id);
        }
    }


}
