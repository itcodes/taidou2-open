using System;

namespace XHConfig
{
    public partial class StudentConfig : IReader
    {
        public StudentItem[] Items;

        public void Reader(string content)
        {
            string[] array = content.Split(';');
            Items = new StudentItem[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                Items[i] = new StudentItem();
                Items[i].Reader(array[i]);
            }
        }
    }

    public partial class StudentItem : IReader
    {
        public int ID;
        public string Name;
        public E_Sex Sex;
        public int Age;

        public void Reader(string content)
        {
            string[] array = content.Split(',');
            ID = int.Parse(array[0]);
            Name = array[1];
            Sex = (E_Sex)(int.Parse(array[2]));
            Age = int.Parse(array[3]);
        }

    }

    public enum E_Sex
    {
        None,
        Man,
        Women,
    }
}
