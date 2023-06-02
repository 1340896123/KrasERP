using Furion.DatabaseAccessor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrasERP.Core.Models
{
    [Table("Property", Schema = "kras")]
    public class BasePropertyInfo : EntityBase
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
        /// <summary>
        /// 占位文本
        /// </summary>

        public string PlaceholderText { get; set; }
        /// <summary>
        /// 网格宽度
        /// </summary>
        public int GridWidth { get; set; }

        /// <summary>
        /// 描述
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// 帮助文本
        /// </summary>

        public string HelpText { get; set; }

        /// <summary>
        /// 必须
        /// </summary>

        public bool Required { get; set; }
        /// <summary>
        /// 唯一
        /// </summary>

        public bool Unique { get; set; }
        /// <summary>
        /// 创建索引
        /// </summary>

        public bool Index { get; set; }
        /// <summary>
        /// 主网格是否显示
        /// </summary>

        public bool Searchable { get; set; }
        /// <summary>
        /// 开启审计
        /// </summary>

        public bool Auditable { get; set; }
        /// <summary>
        /// 是否系统字段
        /// </summary>

        public bool System { get; set; }
        ///// <summary>
        /////权限?
        ///// </summary>
        //[JsonProperty(PropertyName = "permissions")]
        //public PropertyPermissions Permissions { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public bool EnableSecurity { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public Guid SourceID { get; set; }

        public ItemTypeInfo Source { get; set; }
        public Guid Related { get; set; }

        /// <summary>
        /// 正则表达式
        /// </summary>

        public string Regex { get; set; }
        public BasePropertyInfo()
        {
            Required = false;
            Unique = false;
            Searchable = false;
            Auditable = false;
            System = false;
            // Permissions = null;
            EnableSecurity = false;
            // Permissions = new PropertyPermissions();
        }
        /// <summary>
        /// 属性类型
        /// </summary>

        public PropertyDataType PropertyDataType { get; set; } = Models.PropertyDataType.String;
        /// <summary>
        /// 默认值
        /// </summary>

        public string DefaultValue { get; set; }
        /// <summary>
        /// 长度
        /// </summary>

        public int Length { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>

        public string Format { get; set; }
        /// <summary>
        /// 日期默认值
        /// </summary>

        public bool? UseCurrentTimeAsDefaultValue { get; set; }
        /// <summary>
        /// 可以多选
        /// </summary>

        public bool CanMulteSelect { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>

        public Guid DataSource { get; set; }
        /// <summary>
        /// 精度
        /// </summary>

        public int Precision { get; set; }
        /// <summary>
        /// 小数位数
        /// </summary>

        public int Scale { get; set; }

        /// <summary>
        /// 引用外部属性
        /// </summary>

        public Guid ForeignProperty { get; set; }

        ///// <summary>
        ///// 引用外部属性
        ///// </summary>
        //[JsonProperty(PropertyName = "isCurrent")]
        //public Guid IsCurrent { get; set; }

        ///// <summary>
        ///// 引用外部属性
        ///// </summary>
        //[JsonProperty(PropertyName = "configID")]
        //public Guid ConfigID { get; set; }
        ///// <summary>
        ///// 引用外部属性
        ///// </summary>
        //[JsonProperty(PropertyName = "configID")]
        //public Guid generation { get; set; }


        public BasePropertyInfo(BasePropertyInfo Property) : this()
        {
            Id = Property.Id;
            Name = Property.Name;
            Label = Property.Label;
            PlaceholderText = Property.PlaceholderText;
            Description = Property.Description;
            HelpText = Property.HelpText;
            Required = Property.Required;
            Unique = Property.Unique;
            Searchable = Property.Searchable;
            Auditable = Property.Auditable;
            System = Property.System;
            //Permissions = Property.Permissions;
            EnableSecurity = Property.EnableSecurity;
            SourceID = Property.SourceID;
        }

    }


    [Serializable]
    public class PropertyList
    {
        [JsonProperty(PropertyName = "Propertys")]
        public List<BasePropertyInfo> Propertys { get; set; }

        public PropertyList()
        {
            Propertys = new List<BasePropertyInfo>();
        }
    }


    [Serializable]
    public class PropertyPermissions
    {
        public Guid SourceID { get; set; }

        [JsonProperty(PropertyName = "canRead")]
        public List<Guid> CanRead { get; set; }

        [JsonProperty(PropertyName = "canUpdate")]
        public List<Guid> CanUpdate { get; set; }

        public PropertyPermissions()
        {
            CanRead = new List<Guid>();
            CanUpdate = new List<Guid>();
        }
    }
    public class PropertyPermission //: EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }


    }


    public enum PropertyDataType
    {
        [SelectOption(Label = "string")]
        String = 1,
        [SelectOption(Label = "text")]
        Text = 2,
        [SelectOption(Label = "integer")]
        Integer = 3,
        [SelectOption(Label = "float")]
        Float = 4,
        [SelectOption(Label = "decimal")]
        Decimal = 5,
        [SelectOption(Label = "bool")]
        Bool = 6,
        [SelectOption(Label = "date")]
        Date = 7,
        [SelectOption(Label = "image")]
        Image = 8,
        [SelectOption(Label = "md5")]
        MD5 = 9,
        [SelectOption(Label = "list")]
        List = 10,
        [SelectOption(Label = "filterList")]
        FilterList = 11,
        [SelectOption(Label = "foreign")]
        Foreign = 12,
        [SelectOption(Label = "guid")]
        Guid = 13,
        [SelectOption(Label = "relation")]
        Relation = 14,
        [SelectOption(Label = "geography")]
        Geography = 15,
        [SelectOption(Label = "dateTime")]
        DateTime = 16
    }


    //public class StringPropertyInfo : BasePropertyInfo
    //{

    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public PropertyDataType PropertyDataType { get; set; } = Models.PropertyDataType.String;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public string DefaultValue { get; set; }
    //    [JsonProperty(PropertyName = "length")]
    //    public int Length { get; set; }
    //}

    //public class TextPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Text;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new string DefaultValue { get; set; }
    //}
    //public class MD5PropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.MD5;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new string DefaultValue { get; set; }
    //    [JsonProperty(PropertyName = "length")]
    //    public new string Length { get; set; }
    //}
    //public class DatePropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Date;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new DateTime? DefaultValue { get; set; }

    //    [JsonProperty(PropertyName = "format")]
    //    public new string Format { get; set; }

    //    [JsonProperty(PropertyName = "useCurrentTimeAsDefaultValue")]
    //    public new bool? UseCurrentTimeAsDefaultValue { get; set; }
    //}
    //public class DateTimePropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.DateTime;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new DateTime? DefaultValue { get; set; }

    //    [JsonProperty(PropertyName = "format")]
    //    public new string Format { get; set; }

    //    [JsonProperty(PropertyName = "useCurrentTimeAsDefaultValue")]
    //    public new bool? UseCurrentTimeAsDefaultValue { get; set; } = false;
    //}
    //public class ListPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.List;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new string DefaultValue { get; set; }

    //    [JsonProperty(PropertyName = "canMulteSelect")]
    //    public new bool CanMulteSelect { get; set; }

    //    [JsonProperty(PropertyName = "dataSource")]
    //    public new Guid DataSource { get; set; }
    //}
    //public class FloatPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Float;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new double DefaultValue { get; set; }
    //}
    //public class BoolPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Bool;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new bool DefaultValue { get; set; }
    //}
    //public class GuidPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Guid;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new Guid DefaultValue { get; set; }
    //    [JsonProperty(PropertyName = "generateNewId")]
    //    public new bool GenerateNewId { get; set; }
    //}
    //public class FilterListPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.FilterList;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new string DefaultValue { get; set; }

    //    [JsonProperty(PropertyName = "filterColName")]
    //    public new string FilterColName { get; set; }

    //    [JsonProperty(PropertyName = "dataSource")]
    //    public new Guid DataSource { get; set; }
    //}

    //public class DecimalPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Decimal;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new decimal DefaultValue { get; set; }

    //    [JsonProperty(PropertyName = "precision")]
    //    public new int Precision { get; set; }
    //    [JsonProperty(PropertyName = "scale")]
    //    public new int Scale { get; set; }
    //}
    //public class ImagePropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Image;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new Guid DefaultValue { get; set; }
    //}
    //public class IntegerPropertyInfo : BasePropertyInfo 
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Integer;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new int DefaultValue { get; set; }
    //}
    //public class RelationPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set;     } = PropertyDataType.Relation;
    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public new Guid DefaultValue { get; set; }
    //}
    //public class ForeignPropertyInfo : BasePropertyInfo
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public new PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Foreign;
    //    [JsonProperty(PropertyName = "foreignProperty")]
    //    public new Guid ForeignProperty { get; set; }

    //    [JsonProperty(PropertyName = "dataSource")]
    //    public new Guid DataSource { get; set; }
    //}

    //[Serializable]
    //public class GeographyPropertyInfo : BasePropertyInfo 
    //{
    //    [JsonProperty(PropertyName = "propertyDataType")]
    //    public PropertyDataType PropertyDataType { get; set; } = PropertyDataType.Geography; 

    //    [JsonProperty(PropertyName = "defaultValue")]
    //    public string DefaultValue { get; set; }

    //    [JsonProperty(PropertyName = "length")]
    //    public int? Length { get; set; } 

    //    [JsonProperty(PropertyName = "visibleLineNumber")]
    //    public int? VisibleLineNumber { get; set; }

    //    [JsonProperty(PropertyName = "format")]
    //    public GeographyFieldFormat? Format { get; set; }

    //    [JsonProperty(PropertyName = "srid")]
    //    public int SRID { get; set; } = 4326;// ErpSettings.DefaultSRID;
    //}
    [Serializable]
    public enum GeographyFieldFormat
    {
        GeoJSON = 1, // ST_AsGeoJSON, default
        Text = 2, // STAsText
    }

}
