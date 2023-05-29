using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.Models
{
    [Table("ItemType", Schema = "kras")]
    public class ItemTypeInfo : EntityBase
    {

        public new Guid? Id { get; set; }


        public string Name { get; set; } = "";


        public string Label { get; set; } = "";


        public string LabelPlural { get; set; } = "";


        public bool? System { get; set; } = false;


        public string IconName { get; set; } = "";


        public string Color { get; set; } = "";


        //public RecordPermissions RecordPermissions { get; set; }


        public Guid? RecordScreenIdField { get; set; } //If null the ID field of the record is used as ScreenId

        public string KeyedName => Name;
    }

    public class RecordPermissions
    {

        public List<Guid> CanRead { get; set; } = new List<Guid>();


        public List<Guid> CanCreate { get; set; } = new List<Guid>();


        public List<Guid> CanUpdate { get; set; } = new List<Guid>();


        public List<Guid> CanDelete { get; set; } = new List<Guid>();
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
