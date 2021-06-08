using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Furry
{
    internal static class MathParser
    {
        private static string begin =
@"using System;
namespace Parser
{
    public static class LambdaCreator 
    {
        
        private static double sin(double x)
        {
            return Math.Sin(x);
        }

        private static double cos(double x)
        {
            return Math.Cos(x);
        }

        private static double ln(double x)
        {
            return Math.Log(x);
        }

        private static double exp(double x)
        {
            return Math.Exp(x);
        }

        private static double abs(double x)
        {
            return Math.Abs(x);
        }

        private static double sqrt(double x)
        {
            return Math.Sqrt(x);
        }

        private static double arcsin(double x)
        {
            return Math.Asin(x);
        }

        private static double arccos(double x)
        {
            return Math.Acos(x);
        }

        private static double dz1(double x)
        {
            double res = 0;
            if ((-3 < x) && (x < -2))
            {
                res = 1;
            }
            if ((1 < x) && (x < 3))
            {
                res = 2;
            }
            return res;
        }

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