using Furion.DatabaseAccessor;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Npgsql.PostgresTypes.PostgresCompositeType;

namespace KrasERP.Core.Models
{
    [Serializable]
    public class PropertyInfo : EntityBase
    {
        [JsonProperty(PropertyName = "id")]
        public new Guid Id { get; set; }

        [JsonProperty(PropertyName = "sourceId")]
        public string SourceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "placeholderText")]
        public string PlaceholderText { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "helpText")]
        public string HelpText { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool Required { get; set; }

        [JsonProperty(PropertyName = "unique")]
        public bool Unique { get; set; }

        [JsonProperty(PropertyName = "searchable")]
        public bool Searchable { get; set; }

        [JsonProperty(PropertyName = "auditable")]
        public bool Auditable { get; set; }

        [JsonProperty(PropertyName = "system")]
        public bool System { get; set; }

        [JsonProperty(PropertyName = "permissions")]
        public PropertyPermissions Permissions { get; set; }

        [JsonProperty(PropertyName = "enableSecurity")]
        public bool EnableSecurity { get; set; }

        [JsonProperty(PropertyName = "entityName")]
        public string EntityName { get; set; }

        [JsonProperty(PropertyName = "propertyDataType")]
        public PropertyDataType PropertyDataType { get; set; }

        [JsonProperty(PropertyName = "regex")]
        public string Regex { get; set; }

        [JsonProperty(PropertyName = "defaultValut")]
        public string DefaultValut { get; set; }
        public PropertyInfo()
        {
            Required = false;
            Unique = false;
            Searchable = false;
            Auditable = false;
            System = false;
            Permissions = null;
            EnableSecurity = false;
            Permissions = new PropertyPermissions();
        }

        public PropertyInfo(PropertyInfo Property) : this()
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
            Permissions = Property.Permissions;
            EnableSecurity = Property.EnableSecurity;
            EntityName = Property.EntityName;
        }

    }


    [Serializable]
    public class PropertyList
    {
        [JsonProperty(PropertyName = "Propertys")]
        public List<PropertyInfo> Propertys { get; set; }

        public PropertyList()
        {
            Propertys = new List<PropertyInfo>();
        }
    }


    [Serializable]
    public class PropertyPermissions
    {
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
        [SelectOption(Label = "multiValueList")]
        MultiValueList = 13,
        [SelectOption(Label = "guid")]
        GuidField = 14,
        [SelectOption(Label = "relation")]
        RelationField = 15,
        [SelectOption(Label = "geography")]
        GeographyField = 16,
        [SelectOption(Label = "dateTime")]
        DateTime = 17
    }

    public class DatePropertyInfo : PropertyInfo
    {
        [JsonProperty(PropertyName = "fieldType")]
        public static PropertyDataType PropertyDataType { get { return PropertyDataType.DateTime; } }
        [JsonProperty(PropertyName = "defaultValue")]
        public DateTime? DefaultValue { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "useCurrentTimeAsDefaultValue")]
        public bool? UseCurrentTimeAsDefaultValue { get; set; }
    }

    [Serializable]
    public class GeographyField : PropertyInfo
    {
        [JsonProperty(PropertyName = "fieldType")]
        public static PropertyDataType PropertyDataType { get { return PropertyDataType.GeographyField; } }

        [JsonProperty(PropertyName = "defaultValue")]
        public string DefaultValue { get; set; }

        [JsonProperty(PropertyName = "maxLength")]
        public int? MaxLength { get; set; }

        [JsonProperty(PropertyName = "visibleLineNumber")]
        public int? VisibleLineNumber { get; set; }

        [JsonProperty(PropertyName = "format")]
        public GeographyFieldFormat? Format { get; set; }

        [JsonProperty(PropertyName = "srid")]
        public int SRID { get; set; } = 4326;// ErpSettings.DefaultSRID;
    }
    [Serializable]
    public enum GeographyFieldFormat
    {
        GeoJSON = 1, // ST_AsGeoJSON, default
        Text = 2, // STAsText
    }

}
