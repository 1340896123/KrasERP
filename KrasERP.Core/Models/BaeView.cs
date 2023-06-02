using Furion.DatabaseAccessor;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrasERP.Core.Models
{
    [Table("View", Schema = "kras")]
    public class BaeView : EntityBase
    {
        /// <summary>
        /// ID
        /// </summary>

        public new Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public string form_classification { get; set; }

        public ItemTypeInfo Source { get; set; }
        public Guid SourceId { get; set; }
        public BaseForm Related { get; set; }
        public Guid RelatedId { get; set; }

    }
}
