# aivo

Aivo is a simple C# Code Driven library for implementing game AI. It uses .Net 3.5 and is compatible with Unity. It is inspired by  [Fluent-Behaviour-Tree](https://github.com/codecapers/Fluent-Behaviour-Tree/blob/master/src/BehaviourTreeStatus.cs) & [Java Behaviour Trees](https://github.com/gaia-ucm/jbt). 

Introduction to Behaviour Trees can be found from [here](https://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php) about behaviour trees for game AI.

Currently Aivo implements the only basic set of Behaviour Tree Node types.

## Installation

Get release dll from [here](https://github.com/mhjort/aivo/releases/download/0.0.1/aivo-0.0.1.dll).

## Usage

Example for creating a behaviour for a bot AI for Clash Royale kind of game.

Creating the behaviour tree:

```
using AivoTree;
...
var tree = new SelectorNode<BotContext>(
                new SequenceNode<BotContext>(enoughManaToDeployUnit,
                                             lessUnitsThanOpponent,
                                             bestTargetToAttack,
                                             bestUnitToAttack,
                                             deploySelectedUnit));
```

Calling the behaviour tree in every game tick:
```
var context = new BotContext();
var status = tree.tick(tickTime, context);
// Do something with status
```

### Statuses

```
public enum AivoTreeStatus
{
    Success, // The node has finished what it was doing and succeeded.
    Failure, // The node has finished, but failed.
    Running  // The node is still working on something.
}
```

### Composite Nodes

#### Sequence

A sequence will visit each child node in order. If any child node fails it will immediately return failure to the parent node. If the last child node in the sequence succeeds, then the sequence will return success to its parent node.

```
new SequenceNode<Context>(node1, node2, node3);
```

#### Selector

A selector visits child nodes in sequence until it finds one that succeeds. For child nodes that fail it moves forward to the next child node. Returns a failure if none of the child nodes succeed.

```
new SelectorNode<Context>(choice1, choice2, choice3);
```
