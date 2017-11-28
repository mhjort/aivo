using AivoTree;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void RunsActioNode()
        {
            var model = new MyModel();
            var action = new ActionNode<MyModel>((frame, ctx) =>
            {
                ctx.value = frame;
                return AivoTreeStatus.Success;
            });
            Assert.AreEqual(AivoTreeStatus.Success, action.Tick(1, model));
            Assert.AreEqual(1, model.value);
        }
        
        [Test]
        public void SequenceNodeFailsWithFirstFailure()
        {
            var model = new MyModel();
            var sequence = new SequenceNode<MyModel>(new SucceedingNode(), new FailingNode(), new ShouldNotRunNode());
            Assert.AreEqual(AivoTreeStatus.Failure, sequence.Tick(1, model));
        }
        
        [Test]
        public void SequenceNodeSucceedsIfAllNodesSucceed()
        {
            var model = new MyModel();
            var sequence = new SequenceNode<MyModel>(new SucceedingNode(), new SucceedingNode());
            Assert.AreEqual(AivoTreeStatus.Success, sequence.Tick(1, model));
        }
        
        [Test]
        public void SelectorNodeSucceedSWithFirstSucceedingNode()
        {
            var model = new MyModel();
            var sequence = new SelectorNode<MyModel>(new SucceedingNode(), new ShouldNotRunNode());
            Assert.AreEqual(AivoTreeStatus.Success, sequence.Tick(1, model));
        }
        
        [Test]
        public void SelectorNodeFailsIfAllNodesFails()
        {
            var model = new MyModel();
            var sequence = new SelectorNode<MyModel>(new FailingNode());
            Assert.AreEqual(AivoTreeStatus.Failure, sequence.Tick(1, model));
        }
    }

    public class MyModel
    {
        public int value;
    }

    public class FailingNode : TreeNode<MyModel>
    {
        public AivoTreeStatus Tick(int frame, MyModel context)
        {
            return AivoTreeStatus.Failure;
        }
    }
    
    public class SucceedingNode : TreeNode<MyModel>
    {
        public AivoTreeStatus Tick(int frame, MyModel context)
        {
            return AivoTreeStatus.Success;
        }
    }
    
    public class ShouldNotRunNode : TreeNode<MyModel>
    {
        public AivoTreeStatus Tick(int frame, MyModel context)
        {
            throw new AssertionException("Should not be called");
        }
    }
}