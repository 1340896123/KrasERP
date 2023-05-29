using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.DataBase
{
    [Flags]
    public enum ConstraintType
    {
        Union = 'c',
        NotNull= 'n',
        Foreign='f',
        PrimaryKey='p',
        Table = 't',
        Exclusive = 'x',
    }

}
