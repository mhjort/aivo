# aivo

Aivo is a simple C# Code Driven library for implementing game AI. It uses .Net 3.5 and is compatible with Unity. It is inspired by  [Fluent-Behaviour-Tree](https://github.com/codecapers/Fluent-Behaviour-Tree/blob/master/src/BehaviourTreeStatus.cs) & [Java Behaviour Trees](https://github.com/gaia-ucm/jbt). The goal of this library is to make it easy to compose different kinds of behaviours by reusing the pieces as much as possible.

Introduction to Behaviour Trees can be found from [here](https://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php) about behaviour trees for game AI.

Currently Aivo implements the only basic set of Behaviour Tree Node types.

## Installation

Get release dll from [here](https://github.com/mhjort/aivo/releases/download/0.0.2/aivo-0.0.2.dll).

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

#### Inverter

A invertor inverts the result of the node. If the node beeing inverted returns ```running``` inverter returns ```running``` too.

```
new InverterNode<Context>(node);
```

### Leaf Nodes

#### ActionNode

Action nodes run a single command, update the state in context and return the Tree status. Command is defined as a function that takes in a game tick (see Game Time later on) and the context (see Data Context later). 

```
var lessUnitsThanOpponent = new ActionNode<Context>((timeTick, ctx) =>
{
    var playerUnits = ctx.gameModel.PlayerUnits();
    var opponentUnits = ctx.gameModel.OpponentUnits();

    if (playerUnits.Count() < opponentUnits.Count())
    {
        return AivoTreeStatus.Success;
    }
        return AivoTreeStatus.Failure;
});
```

### Data Context

As the goal of the library is to make it easy to reuse parts of trees it is not encouraged to access global variables from the nodes. Instead the user defined context object is passed to all the nodes . The node should read the latest state from the context object and update the context accordingly. 

Data context is highly dependent on the case so there are no limits for it. It is just class that you can define.

For example:

```
public class MyContext
{
    public String selectedTargetToAttack;
    public GameModel gameModel;
}
```

### Game time

You should pass game time for every ```Tick``` call. The time can be absolute time (for example in ms) or game frame number. This depends on your implementation. However, Aivo expects that number increases with every tick at least by one.

## Roadmap

The short term goal is to add support for random order selectors and sequences. Also the current version is not performance optimized yet.

## Contribute

Use [GitHub issues](https://github.com/mhjort/aivo/issues) and [Pull Requests](https://github.com/mhjort/aivo/pulls).

## License

Copyright (C) 2017 Markus Hjort

Distributed under the MIT License.
