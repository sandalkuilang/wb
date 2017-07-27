namespace Common.Database.SqlLoader
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class FileSqlLoader : BaseSqlLoader
    {  

        public string Path { get; set; }

        public FileSqlLoader(string name, string path) : base(name)
        {
            this.Path = path;
            base.AddExtension(".sql");
            base.AddExtension(".txt");
            base.AddExtension(".inc"); // add new extention 
        }

        public override string GetContent(string name)
        {
            try
            {
                string[] files = Directory.GetFiles(this.Path);
                string str = string.Empty;
                for (int i = 0; i < (files.Length); i++)
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(files[i]) == name)
                    {
                        for (int j = 0; j < base.Extensions.Count; j++)
                        {
                            if (System.IO.Path.GetExtension(files[i]) == base.Extensions[j])
                            {
                                return (str = File.ReadAllText(files[i]));
                            }
                        }
                    }
                }
                return str;
            }
            catch (DirectoryNotFoundException)
            {
                return string.Empty;
            }
        }

        public override StreamReader GetStream(string filename)
        {
            throw new NotImplementedException();
        }
    }
}

