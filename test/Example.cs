using AivoTree;

namespace Test
{
    public class Example
    {
        public static TreeNode<BotContext> createCounterAttackBehaviour()
        {
            var enoughManaToDeployUnit = new ActionNode<BotContext>((timeTick, ctx) => AivoTreeStatus.Success);
            var lessUnitsThanOpponent = new ActionNode<BotContext>((timeTick, ctx) => AivoTreeStatus.Success);
            var bestTargetToAttack = new ActionNode<BotContext>((timeTick, ctx) => AivoTreeStatus.Success);
            var bestUnitToAttack = new ActionNode<BotContext>((timeTick, ctx) => AivoTreeStatus.Success);
            var deploySelectedUnit = new ActionNode<BotContext>((timeTick, ctx) => AivoTreeStatus.Success);

            return new SelectorNode<BotContext>(
                new SequenceNode<BotContext>(enoughManaToDeployUnit, lessUnitsThanOpponent, bestTargetToAttack,
                    bestUnitToAttack, deploySelectedUnit));
        }
    }


    public class BotContext{}
}