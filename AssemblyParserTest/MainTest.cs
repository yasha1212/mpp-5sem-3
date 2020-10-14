using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyParserLib;
using System.Collections.Generic;
using AssemblyParserLib.TreeParts;
using System.Linq;

namespace AssemblyParserTest
{
    public static class CustomExtensions
    {
        public static void DoNothing(this MainTest instance, int x, string s) { }
    }

    public class GenericClass<T>
    {
        private T GetValueMethod(T data)
        {
            return data;
        }
    }

    [TestClass]
    public class MainTest
    {
        private int i;
        private string s;
        private AssemblyTree tree = (new AssemblyParser()).ParseFromFile(@"D:\University\GitRepos\mpp-5sem-3\AssemblyParserTest\bin\Debug\AssemblyParserTest.dll");

        private List<T> GetListGenericMethod<T>()
        {
            return new List<T>();
        }

        [TestMethod]
        public void TestAssemblyParameters()
        {
            Assert.AreEqual(1, tree.Namespaces.Count);
            Assert.AreEqual("AssemblyParserTest", tree.Namespaces[0].Name);
            Assert.AreEqual(4, tree.Namespaces[0].DataTypes.Count);
        }

        [TestMethod]
        public void TestGenericClass()
        {
            Assert.AreEqual("GenericClass<T>", tree.Namespaces[0].DataTypes[1].Name);
            Assert.AreEqual("T GetValueMethod(T)", tree.Namespaces[0].DataTypes[1].Methods[0].Signature);
            Assert.AreEqual(0, tree.Namespaces[0].DataTypes[1].Fields.Count);
            Assert.AreEqual(0, tree.Namespaces[0].DataTypes[1].Properties.Count);
        }

        [TestMethod]
        public void TestMainClass()
        {
            Assert.AreEqual(3, tree.Namespaces[0].DataTypes[2].Fields.Count);
            Assert.AreEqual(0, tree.Namespaces[0].DataTypes[2].Properties.Count);
            Assert.AreEqual("MainTest", tree.Namespaces[0].DataTypes[2].Name);
            Assert.AreEqual("List<T> GetListGenericMethod<T>()", tree.Namespaces[0].DataTypes[2].Methods[0].Signature);
            Assert.AreEqual("Int32 i", tree.Namespaces[0].DataTypes[2].Fields[0].Signature);
            Assert.AreEqual("String s", tree.Namespaces[0].DataTypes[2].Fields[1].Signature);
            Assert.AreEqual("AssemblyTree tree", tree.Namespaces[0].DataTypes[2].Fields[2].Signature);
        }

        [TestMethod]
        public void TestExtensionMethod()
        {
            Assert.AreEqual(1, tree.Namespaces[0].DataTypes[2].Methods.Where(m => m.Signature.Contains("- Extension")).ToList().Count);
        }
    }
}
