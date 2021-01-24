# Groups

为了从实体集合访问实体，我们具有组的概念（“ IGroup”的实现）。由于每个系统都需要一个组来访问实体，因此它们是一个相当重要的难题。

例如，假设我想要一个“ Player”的概念，它实际上可以表示为具有“ IsPlayerControlled”，“ IsSceneActor”和“ HasStatistics”组件的实体。如果我们假装说“ IsPlayerControlled”意味着玩家控制了这个实体，那么“ IsSceneActor”就意味着您在场景中拥有一个“ GameObject”，它会做一些随机的事情，而“ HasStatistics”则包含诸如健康，法力，强度等。现在，如果我们假设用它们表示“玩家”组，则可以将“ NPC”表示为仅具有“ IsSceneActor”和“ HasStatistics”的实体，这样您就可以从高层次看待您的实体但最好地利用更细粒度的组件。

## How groups work

因此，组非常简单，它们只是POCO，它们描述了您希望从实体集合中匹配的组件类型。因此，如果您有数百个实体，每个实体都具有不同的组件，则可以使用组来表达系统的高级意图。

因此，实体集合提供了一种传递组并获取与该组匹配的实体的方法，如前所述，主要的匹配机制是通过组件实现的，但还有实体匹配的概念，这是实验性的，您可以找到更多关于这些在此页面的下方。

## Creating Groups

创建组有几种不同的方法，以下是一些常用方法。

### Instantiate a group

有一个实现“ IGroup”的“ Group”类，可以实例化该类并传递您要定位的任何组件，例如：

```csharp
var group = new Group(typeof(SomeComponent));
```

这里还有一些辅助方法，因此您可以根据需要通过扩展方法添加组件类型，如下所示：

```csharp
var group = new Group()
    .WithComponent<SomeComponent>()
    .WithoutComponent<SomeOtherComponent();
```

这是构建器方法和实例化方法之间的过渡，如果您想从现有组中快速创建新组，这通常很方便。如果需要，它还支持通过参数一次添加许多组件。

### Use the GroupBuilder

因此，还有一个`GroupBuilder`类，可以简化创建复杂的组的过程，易于使用，并允许您流畅地表达复杂的组设置，如下所示：

```csharp
var group = new GroupBuilder()
    .WithComponent<SomeComponent>()
    .WithComponent<SomeOtherComponent>()
    .Build();
```

### Implement your own IGroup

因此，如果您将大量使用相同的分组，则可以自己实现IGroup，这将意味着减少使用上述概念来构建分组的麻烦。

创建自己的组非常简单，只需实现两个getter：

```csharp
public class MyGroup : IGroup
{
    public IEnumerable<Type> RequiredComponents {get;} =  return new[] { typeof(SomeComponent), typeof(SomeOtherComponent) };
        
    public IEnumerable<Type> ExcludedComponents {get;} =  return new[] { typeof(SomeComponentIDontWant) };
}
```

如您所见，您现在可以实例化`new MyGroup（）;`，每个人都很高兴。

## Required/Excluded Components and TargettedEntities

因此，正如大多数实体所提到的，受其组件类型的约束，这是“ RequiredComponents”和“ ExcludedComponents”部分，但是还有一个“ TargettedEntities”概念，该概念从本质上讲进一步约束了一步，使您可以进一步约束匹配组件的实体。

TargettedEntities属性只是一个接受实体的谓词，如果匹配或不匹配，则返回。当前，在将订阅传递给系统执行之前会检查这些内容，因此您可以表达一些复杂的约束，而不必在每个系统中都明确。

因此，例如，假设您想要一个仅对带有“ health == 0”的字符作出反应的系统，您可以进行基于组件的分组，然后在系统反应或执行阶段检查一下health == 0以及是否不会返回，但是如果您有2个系统希望对字符死亡的概念做出反应，那么最终将导致大量重复代码。

因此，此功能可为您带来好处，因为您可以在组级别表达此排序规则，因此可以确保除非谓词匹配，否则系统甚至不会在实体上执行。

### Warning

目前已实现此功能，因为它提供了一种更好的方式来表达高级分组逻辑，但是可能会出现一些利基用例，这些情况会引起比应有的混乱，因此，随着计算的进行，此功能可能会被删除或更改。组与这种功能有些重叠。