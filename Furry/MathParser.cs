﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Furry
{
    internal class MathParser
    {
        private static string begin =
@"using System;
namespace Parser
{
    public static class LambdaCreator 
    {
        public static Func<double, double> Create()
        {
            return (x)=>";
        private static string end = @";
        }
    }
}";
        public static Func<double, double> MakeExpr(string Expr)
        {
            string middle = Expr;
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateInMemory = true
            };
            parameters.ReferencedAssemblies.Add("System.dll");
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, begin + middle + end);
            Type cls = results.CompiledAssembly.GetType("Parser.LambdaCreator");
            MethodInfo method = cls.GetMethod("Create", BindingFlags.Static | BindingFlags.Public);
            Delegate f = method.Invoke(null, null) as Delegate;
            return (Func<double, double>)f;
        }
    }
}