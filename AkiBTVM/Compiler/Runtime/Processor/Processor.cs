using System;
namespace Kurisu.AkiBT.Compiler
{
    internal enum VariableCompileType
    {
        Int,Float,Bool,String,Vector3
    }
    internal abstract class Processor:IDisposable
    {
        protected AkiBTCompiler Compiler{get;private set;}
        protected Scanner Scanner{get;private set;}
        protected int CurrentIndex=>Scanner.CurrentIndex;
        protected int TotalCount=>Scanner.TotalCount;
        protected string CurrentToken=>Scanner.CurrentToken;
        internal void Init(AkiBTCompiler compiler,Scanner scanner)
        {
            this.Compiler=compiler;
            this.Scanner=scanner;
            OnInit();
        }
        protected virtual void OnInit(){}
        public void Dispose()
        {
            Compiler.PushProcessor(this);
        }
    }
    internal static class ScannerExtension
    {
        internal static bool TryGetVector3(this Scanner scanner,out Vector3 vector3)
        {
            return Vector3Helper.TryGetVector3(scanner,out vector3);
        } 
        internal static Vector3 GetVector3(this Scanner scanner)
        {
            return Vector3Helper.GetVector3(scanner);
        }
        internal static bool TryGetVector2(this Scanner scanner,out Vector2 vector2)
        {
            return Vector2Helper.TryGetVector2(scanner,out vector2);
        } 
        internal static Vector2 GetVector2(this Scanner scanner)
        {
            return Vector2Helper.GetVector2(scanner);
        }
    }
}
