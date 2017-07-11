using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JsonCS.JCS
{
    public class JcsClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("fields")]
        public Class.JcsField[] Fields { get; set; }
        [JsonProperty("methods")]
        public Class.JcsMethod[] Methods { get; set; }

        public Type Compile(ModuleBuilder m)
        {
            return BuildType(DefineType(m)).CreateType();
        }
        private TypeBuilder DefineType(ModuleBuilder m)
        {
            return m.DefineType(Name, TypeAttributes.Public, typeof(object));
        }
        private TypeBuilder BuildType(TypeBuilder type, Dictionary<string, TypeBuilder> passtypes = null)
        {
            type.SetCustomAttribute(Type.GetType("System.SerializableAttribute", false, true).GetConstructor(new Type[] { }), new byte[] { });
            if (Fields != null)
            {
                foreach (var field in Fields)
                {
                    Type t = Type.GetType(field.FieldType, false, true);
                    if (t == null && !passtypes.ContainsKey(field.FieldType))
                        throw new JcsClassbuilderException($"Could not find type {field.FieldType} for {type.Name}");
                    type.DefineField(field.FieldName, t ?? passtypes[field.FieldType], field.AccessAttribute());
                }
            }
            if (Methods != null)
            {
                foreach (var method in Methods)
                {
                    var mtb = type.DefineMethod(
                        method.Name,
                        method.AccessAttribute(),
                        Type.GetType(method.ReturnType, false, true),
                        method.Params.Select(x => Type.GetType(method.ReturnType, false, true)).ToArray()
                        );
                    mtb.CreateMethodBody(method.IL, method.IL.Length);
                }
            }
            return type;
        }
        public static Type[] Compile(JcsClass[] types, ModuleBuilder m)
        {
            var typedict = new Dictionary<string, TypeBuilder>();
            foreach (var type in types)
            {
                var newlybuilttype = type.DefineType(m);
                typedict.Add(type.Name, newlybuilttype);
            }
            foreach (var type in types)
            {
                type.BuildType(typedict[type.Name], typedict);
            }
            return typedict.Values.Select(x => x.CreateType()).ToArray();
        }

        public static JcsClass Decompile(Type type)
        {
            return new JcsClass()
            {
                Name = type.FullName,
                Fields = type.GetRuntimeFields().Select(x => 
                    new Class.JcsField()
                    {
                        FieldName = x.Name,
                        FieldType = x.FieldType.FullName,
                        FieldAccess = x.IsPublic ? "public" : "private"
                    }
                ).ToArray(),
                Methods = type.GetRuntimeMethods().Where(x => x.GetMethodBody() != null).Select(mth => 
                    new Class.JcsMethod()
                    {
                        Name = mth.Name,
                        ReturnType = mth.ReturnType.FullName,
                        Access = mth.IsPublic ? "public" : "private",
                        Params = mth.GetParameters().Select(prm =>
                            new Class.JcsParam()
                            {
                                Name = prm.Name,
                                Type = prm.ParameterType.FullName
                            }
                        ).ToArray(),
                        IL = mth.GetMethodBody().GetILAsByteArray()
                    }).ToArray()
            };
        }
    }
}
namespace JsonCS.JCS.Ext
{
    public static class Ext
    {
        public static Type[] Compile(this JcsClass[] types, ModuleBuilder m) =>
            JcsClass.Compile(types, m);
    }
}
