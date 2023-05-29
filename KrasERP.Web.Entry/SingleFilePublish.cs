using Furion;
using System.Reflection;

namespace KrasERP.Web.Entry
{
    public class SingleFilePublish : ISingleFilePublish
    {
        public Assembly[] IncludeAssemblies()
        {
            return Array.Empty<Assembly>();
        }

        public string[] IncludeAssemblyNames()
        {
            return new[]
            {
            "KrasERP.Application",
            "KrasERP.Core",
            "KrasERP.EntityFramework.Core",
            "KrasERP.Web.Core"
        };
        }
    }
}