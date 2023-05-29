using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.Models
{
    public class ItemTypeInfo : EntityBase
    {
        [JsonProperty(PropertyName = "id")]
        public new Guid? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = "";

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = "";

        [JsonProperty(PropertyName = "labelPlural")]
        public string LabelPlural { get; set; } = "";

        [JsonProperty(PropertyName = "system")]
        public bool? System { get; set; } = false;

        [JsonProperty(PropertyName = "iconName")]
        public string IconName { get; set; } = "";

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; } = "";

        [JsonProperty(PropertyName = "recordPermissions")]
        public RecordPermissions RecordPermissions { get; set; }

        [JsonProperty(PropertyName = "record_screen_id_field")]
        public Guid? RecordScreenIdField { get; set; } //If null the ID field of the record is used as ScreenId

       
    }
    [Serializable]
    public class RecordPermissions
    {
        [JsonProperty(PropertyName = "canRead")]
        public List<Guid> CanRead { get; set; } = new List<Guid>();

        [JsonProperty(PropertyName = "canCreate")]
        public List<Guid> CanCreate { get; set; } = new List<Guid>();

        [JsonProperty(PropertyName = "canUpdate")]
        public List<Guid> CanUpdate { get; set; } = new List<Guid>();

        [JsonProperty(PropertyName = "canDelete")]
        public List<Guid> CanDelete { get; set; } = new List<Guid>();

        public static implicit operator RecordPermissions(List<string> v)
        {
            throw new NotImplementedException();
        }
    }


    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SelectOptionAttribute : Attribute
    {
        private string _label = string.Empty;
        private string _iconClass = string.Empty;
        private string _color = string.Empty;

        public virtual string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public virtual string IconClass
        {
            get { return _iconClass; }
            set { _iconClass = value; }
        }

        public virtual string Color
        {
            get { return _color; }
            set { _color = value; }
        }
    }
}
