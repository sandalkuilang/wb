namespace Common.Database.SqlLoader
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class BaseSqlLoader : IFileLoader
    {
        private List<string> extensions = new List<string>();
        private string name;

        public BaseSqlLoader(string name)
        {
            this.name = name;
        }

        public void AddExtension(string ext)
        {
            if (!this.extensions.Contains(ext))
            {
                this.extensions.Add(ext);
            }
        }

        public abstract string GetContent(string filename);
        public abstract StreamReader GetStream(string name);
        public string GetName()
        {
            return this.name;
        }

        public string Load(string name)
        {
            return this.GetContent(name);
        }

        public void RemoveExtension(string ext)
        {
            if (this.extensions.Contains(ext))
            {
                this.extensions.Remove(ext);
            }
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public List<string> Extensions
        {
            get
            {
                return this.extensions;
            }
            set
            {
                this.extensions = value;
            }
        }
    }
}

