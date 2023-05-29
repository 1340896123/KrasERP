using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Form", Schema = "kras")]
    public class BaseForm: EntityBase
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
        /// 描述
        /// </summary>

        public string Description { get; set; }
    }
}
