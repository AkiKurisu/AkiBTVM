using System.Collections.Generic;
namespace Kurisu.AkiBT.Compiler
{
    internal class AutoProcessor : Processor
    {
        private bool rootProcessed;
        protected sealed override void OnInit()
        {
            rootProcessed=false;
            Process();
        }
        private void Process()
        {
            while(CurrentIndex<TotalCount-1)
            {
                Scanner.MoveNextNoSpace();
                if(CurrentIndex>=TotalCount-1)return;
                if(Scanner.TryGetVariableType().HasValue)
                {
                    Scanner.MoveBack();
                    using(ReferencedVariableProcessor processor=Compiler.GetProcessor<ReferencedVariableProcessor>(Compiler,Scanner)){};
                    continue;
                }
                if(Scanner.IsNodeType())
                {
                    Scanner.MoveBack();
                    using(NodeProcessor nodeProcessor=Compiler.GetProcessor<NodeProcessor>(Compiler,Scanner))
                    {
                        var reference=nodeProcessor.GetNode();
                        if(!rootProcessed)
                        {
                            rootProcessed=true;
                            ProcessRoot(reference);
                        }
                    }
                    continue;
                }
            }
        }
        private void ProcessRoot(Reference reference)
        {
            var node=new Node();
            node.data=new Dictionary<string, object>();
            node.data["child"]=reference;
            Compiler.RegisterRoot(node);
        }
    }
}
