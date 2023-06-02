using Furion.DatabaseAccessor;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrasERP.Core.Models
{
    [Table("Field", Schema = "kras")]
    public class BaseField : EntityBase
    {
        /// <summary>
        /// ID
        /// </summary>

        public new Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        public string Css { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public FieldType FieldType { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public BasePropertyInfo PropertyInfo { get; set; }

        public Guid PropertyInfoId { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public BaseForm Source { get; set; }

        public Guid SourceId { get; set; }
    }

    public enum FieldType
    {
        Text,
        List,
        TextArea,
        Radio,
        CheckBox
    }
}
