using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Web.Entry.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomDynamicColumnsObjectData : DynamicColumnsObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Fix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CustomDynamicColumnsObjectData() : this("", new()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fix"></param>
        /// <param name="data"></param>
        public CustomDynamicColumnsObjectData(string fix, Dictionary<string, object> data)
        {
            Fix = fix;
            Columns = data;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public override object GetValue(string propertyName)
        {
            return propertyName == nameof(Fix) ? Fix : base.GetValue(propertyName);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public override void SetValue(string propertyName, object value)
        {
            if (propertyName == nameof(Fix))
                Fix = value?.ToString();
            Columns[propertyName] = value;
        }
    }

}
