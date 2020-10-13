using AssemblyParserLib.TreeParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace AssemblyParserLib
{
    public class AssemblyParser
    {
        private const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

        public AssemblyTree ParseFromFile(string path)
        {
            var asm = Assembly.LoadFrom(path);
            var asmTree = new AssemblyTree();

            asm.GetTypes().Select(t => t.Namespace).Distinct().Where(n => n != null).ToList().ForEach(n => 
                {
                    asmTree.Namespaces.Add(new Namespace(n));
                    asm.GetTypes().Where(t => t.Namespace == n).ToList().ForEach(t => 
                    {
                        asmTree.Namespaces.Last().DataTypes.Add(new DataType(GetTypeName(t)));
                        asmTree.Namespaces.Last().DataTypes.Last().Fields = GetFields(t);
                        asmTree.Namespaces.Last().DataTypes.Last().Properties = GetProperties(t);
                        asmTree.Namespaces.Last().DataTypes.Last().Methods = GetMethods(t);
                    });
                });

            return asmTree;
        }

        private string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                var parameters = type.GetGenericArguments().Select(a => a.Name);
                var genericTypeName = type.GetGenericTypeDefinition().Name;

                return String.Format("{0}<{1}>", genericTypeName.Contains("`") ? 
                                     genericTypeName.Substring(0, genericTypeName.IndexOf("`")) : genericTypeName, 
                                     String.Join(", ", parameters));
            }
            else
            {
                return type.Name;
            }
        }

        private List<Field> GetFields(Type type)
        {
            var fields = new List<Field>();
            var fieldsInfo = type.GetFields(FLAGS);

            fieldsInfo.ToList().ForEach(f => fields.Add(new Field(f.Name, GetTypeName(f.FieldType))));

            return fields;
        }

        private List<Property> GetProperties(Type type)
        {
            var properties = new List<Property>();
            var propertiesInfo = type.GetProperties(FLAGS);

            propertiesInfo.ToList().ForEach(p => properties.Add(new Property(p.Name, GetTypeName(p.PropertyType))));

            return properties;
        }

        private List<Method> GetMethods(Type type)
        {
            var methods = new List<Method>();
            var methodsInfo = type.GetMethods(FLAGS);

            methodsInfo.ToList().ForEach(m => methods.Add(new Method(m.Name, GetSignature(m))));

            return methods;
        }

        private string GetSignature(MethodInfo method)
        {
            string[] parameters = method.GetParameters()
                                        .Select(p => GetTypeName(p.ParameterType))
                                        .ToArray();

            string methodName = method.Name;

            if (method.IsGenericMethod)
            {
                methodName += String.Format("<{0}>", String.Join(", ", method.GetGenericArguments().Select(a => a.Name)));
            }

            return String.Format("{0} {1}({2})", GetTypeName(method.ReturnType), methodName, String.Join(", ", parameters));
        }
    }
}
