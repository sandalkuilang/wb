namespace Common.Database.SqlLoader
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class ResourceSqlLoader : BaseSqlLoader
    {
        private Assembly currentAssembly;
        private string folder;

        public ResourceSqlLoader(string name, string folder, Assembly currentAssembly) : base(name)
        {
            this.folder = folder;
            this.currentAssembly = currentAssembly;
            base.AddExtension("sql");
            base.AddExtension("txt");
            base.AddExtension("inc");
        }

        public override string GetContent(string name)
        {
            string str = string.Empty;
            StreamReader reader = Read(name);
            if (reader != null)
            {
                str = reader.ReadToEnd();
                reader.Dispose();
            } 
            return str;
        }

        public override StreamReader GetStream(string name)
        {
            return Read(name);
        }

        private StreamReader Read(string name)
        { 
            StreamReader reader = null;
            StringBuilder builder = new StringBuilder();
            string assemblyName = this.currentAssembly.GetName().Name;
            for (int i = 0; i < base.Extensions.Count; i++)
            {
                builder.Clear();
                if (string.IsNullOrEmpty(this.folder))
                {
                    if (!string.IsNullOrEmpty(assemblyName))
                    {
                        builder.Append(string.Join(".", new string[] { assemblyName, name, base.Extensions[i] }));
                    }
                    else
                    {
                        builder.Append(string.Join(".", new string[] { name, base.Extensions[i] }));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(assemblyName))
                    {
                        builder.Append(string.Join(".", new string[] { assemblyName, this.folder, name, base.Extensions[i] }));
                    }
                    else
                    {
                        builder.Append(string.Join(".", new string[] { this.folder, name, base.Extensions[i] }));
                    }
                }

                Stream manifestResourceStream = this.currentAssembly.GetManifestResourceStream(builder.ToString());
                if (manifestResourceStream != null)
                {
                    reader = new StreamReader(manifestResourceStream);
                    break;
                    //manifestResourceStream.Dispose();
                }
                 
            }
            return reader;
        }

    }
}

