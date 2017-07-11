using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection.Emit;
using System.Threading;
using System.Reflection;

using JsonCS.JCS.Ext;
namespace JsonCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var classes = Directory.GetFiles("json\\class")
                .Select(v => JsonConvert.DeserializeObject<JCS.JcsClass>(File.ReadAllText(v)))
                .ToArray();
            var assembly = Thread.GetDomain().DefineDynamicAssembly(new AssemblyName("JSONobjects"), AssemblyBuilderAccess.RunAndSave);
            var mdb = assembly.DefineDynamicModule("JSONmodule");

            var compiledObjects = classes.Compile(mdb).ToDictionary(x => x.Name);

            dynamic someobj = Activator.CreateInstance(compiledObjects["SomeObject"]);
            dynamic someinnerobj = Activator.CreateInstance(compiledObjects["InnerObject"]);
            someinnerobj.Str = "nested objects";
            someobj.Inside = someinnerobj;
            someobj.Name = "Hello World!";


            var c = JCS.JcsClass.Decompile(typeof(string));
            string s = JsonConvert.SerializeObject(c);
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
