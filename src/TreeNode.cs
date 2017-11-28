namespace AivoTree
{
    public interface TreeNode<T>
    {
        AivoTreeStatus Tick(int frame, T context);
    }
}