using System;

namespace AivoTree
{
    public class ActionNode<T> : TreeNode<T>
    {
        private readonly Func<int, T, AivoTreeStatus> _fn;

        public ActionNode(Func<int, T, AivoTreeStatus> fn)
        {
            _fn = fn;
        }
        
        public AivoTreeStatus Tick(int frame, T context)
        {
            return _fn(frame, context);
        }
    }
}