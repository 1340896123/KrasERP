using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.EQL
{
    public class SQLInterpreter
    {
        public static void Interpret(string input)
        {
            var sqlgr = new SqlGrammar();
         
            LanguageData language = new LanguageData(sqlgr);
            var parser = new Parser(language);
            var query = "SELECT * FROM customers WHERE age > 18 and name a in ('123','456')";
            var parseTree = parser.Parse(query);

            if (parseTree.Status == ParseTreeStatus.Error)
            {
                Console.WriteLine("Error: " + parseTree.ParserMessages[0].Message);
                return;
            }



            // TODO: Implement interpretation logic here
            Console.WriteLine("Interpretation successful!");
        }
    }
}
