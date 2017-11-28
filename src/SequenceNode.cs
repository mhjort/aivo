using System.Linq;

namespace AivoTree
{
    public class SequenceNode<T> : TreeNode<T>
    {
        private readonly TreeNode<T>[] _nodes;

        public SequenceNode(params TreeNode<T>[] nodes)
        {
            _nodes = nodes;
        }
        
        public AivoTreeStatus Tick(int frame, T context)
        {
            return _nodes.Aggregate(AivoTreeStatus.Success, (acc, curr) =>
            {
                if (acc == AivoTreeStatus.Success)
                {
                    return curr.Tick(frame, context);
                }
                return acc;
            });
        }
    }
}