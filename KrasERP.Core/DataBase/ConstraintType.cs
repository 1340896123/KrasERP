using System;

namespace KrasERP.Core.DataBase
{
    [Flags]
    public enum ConstraintType
    {
        Union = 'c',
        NotNull = 'n',
        Foreign = 'f',
        PrimaryKey = 'p',
        Table = 't',
        Exclusive = 'x',
    }

}
