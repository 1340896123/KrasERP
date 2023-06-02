using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.Models
{

    [Table("RelationshipType", Schema = "kras")]
    public class RelationshipType:IEntity
    {
        public new Guid? Id { get; set; }


        public string Name { get; set; } = "";


        public string Label { get; set; } = "";


        public string LabelPlural { get; set; } = "";


        public bool? System { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>

        public Guid SourceID  { get; set; }

        public ItemTypeInfo Source { get; set; }
        public ItemTypeInfo Related { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public Guid RelatedId { get; set; }

    }
}
