using AssemblyParserLib.TreeParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AssemblyParserLib
{
    public class AssemblyParser
    {
        public AssemblyTree ParseFromFile(string path)
        {
            var asm = Assembly.LoadFrom(path);
            var asmTree = new AssemblyTree();

            var types = asm.GetTypes();
            GetNamespaces(types).ForEach(n => asmTree.Namespaces.Add(new Namespace(n)));

            foreach (var namespaceNode in asmTree.Namespaces)
            {
                foreach (var type in types)
                {
                    if (type.Namespace == namespaceNode.Name)
                    {
                        namespaceNode.DataTypes.Add(new DataType(GetTypeName(type)));
                        namespaceNode.DataTypes[namespaceNode.DataTypes.Count - 1].Fields = GetFields(type);
                        namespaceNode.DataTypes[namespaceNode.DataTypes.Count - 1].Properties = GetProperties(type);
                        namespaceNode.DataTypes[namespaceNode.DataTypes.Count - 1].Methods = GetMethods(type);
                    }
                }
            }

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
            var fieldsInfo = type.GetFields(BindingFlags.Public | BindingFlags.Instance 
                                            | BindingFlags.Static | BindingFlags.NonPublic);

            fieldsInfo.ToList().ForEach(f => fields.Add(new Field(f.Name, GetTypeName(f.FieldType))));

            return fields;
        }

        private List<Property> GetProperties(Type type)
        {
            var properties = new List<Property>();
            var propertiesInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance
                                                    | BindingFlags.Static | BindingFlags.NonPublic);

            propertiesInfo.ToList().ForEach(p => properties.Add(new Property(p.Name, GetTypeName(p.PropertyType))));

            return properties;
        }

        private List<Method> GetMethods(Type type)
        {
            var methods = new List<Method>();
            var methodsInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance
                                              | BindingFlags.Static | BindingFlags.NonPublic);

            methodsInfo.ToList().ForEach(m => methods.Add(new Method(GetSignature(m))));

            return methods;
        }

        private string GetSignature(MethodInfo method)
        {
            string[] parameters = method.GetParameters()
                                        .Select(p => String.Format("{0} {1}", GetTypeName(p.ParameterType), p.Name))
                                        .ToArray();

            return String.Format("{0} {1}({2})", GetTypeName(method.ReturnType), method.Name, String.Join(", ", parameters));
        }

        private List<String> GetNamespaces(Type[] types)
        {
            return types.Select(t => t.Namespace)
                        .Distinct()
                        .Where(n => n != null)
                        .ToList();
        }

    }
}
