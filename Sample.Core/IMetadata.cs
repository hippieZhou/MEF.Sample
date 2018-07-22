using System;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace Sample.Core
{
    /// <summary>
    /// 使用 [MetadataViewImplementation(typeof(CustomExportMetadata))]，则需要修改下面对应的相应不应该使用相应形象
    /// </summary>
    public interface IMetadata
    {
        [DefaultValue(0)]
        int Priority { get; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CustomExportMetadata : ExportAttribute, IMetadata
    {
        public int Priority { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public string Version { get; private set; }

        public CustomExportMetadata() : base(typeof(IMetadata))
        {
        }

        public CustomExportMetadata(int priority) : this()
        {
            this.Priority = priority;
        }

        public CustomExportMetadata(int priority, string name) : this(priority)
        {
            this.Name = name;
        }

        public CustomExportMetadata(int priority, string name, string description) : this(priority, name)
        {
            this.Description = description;
        }

        public CustomExportMetadata(int priority, string name, string description, string author) : this(priority, name, description)
        {
            this.Author = author;
        }

        public CustomExportMetadata(int priority, string name, string description, string author, string version) : this(priority, name, description, author)
        {
            this.Version = version;
        }
    }
}