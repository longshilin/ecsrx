# Observable Groups

因此，现在您知道了什么是实体，以及如何获得它们，值得一提的是遍历可观察组的工作方式以及实体流如何通过生态系统。

## Filtration Flow

```
IObservableGroupManager 	<-  This contains the database which contains all collections, which in turn contains ALL entities
     |
     |
IObservableGroup      		<-  This filters all entities down to only ones which are within the group
     |                    		i.e All entities which contain PlayerComponent
     |
IComputedGroup        		<-  This acts as another layer of filtration on an IObservableGroup
                          		i.e Top 5 entities with PlayerComponent sorted by Score
```

## IObservableGroupManager

可观察的组管理器维护着一个“ IObservableGroup”的集合，因此如果您有5个系统都使用相同的组，则实际上只有1个“ IObservableGroup”实例在它们之间共享。

## IObservableGroup

可观察组是在“ IEntityCollectionManager”内部创建并由其维护的，并公开了与关联组匹配的所有实体。它还公开了可观察到的东西，以表示何时从基础组中添加或删除实体。

在内部，可观察组观察实体，以查看何时将其添加到池中以及何时更改其组件。发生这种情况时，它将检查它是否影响当前组，如果是，它将相应地更新其内部列表。

这具有巨大的性能优势，因为这将使您无需评估到基础池中的linq链，而只给您当前匹配的所有实体的缓存列表，同时考虑到这些可观察的组在需要相同基础组件的任何系统之间共享它可以节省很多资源。

## IComputedGroup

计算的组是手动创建的，并且需要一个“ IObservableGroup”作为其查询的基础。提供它是为了使您能够过滤掉组级别并获得更特定的数据集，而不必对各种系统中的查找逻辑进行硬编码。

一般用例可能是这样的:

- 获得5位得分最高的球员
- 在玩家范围内找到敌人
- 获取当前小队内的所有单位

它旨在成为您使用自己的过滤逻辑实现的接口，并且在内部进行缓存并公开其自己的add/removed状态处理程序，因此您可以像ObservableGroup一样订阅它们。

### `ComputedGroup`

此实现具有内置的缓存，因此它将尝试保留符合过滤要求的实体的预先评估列表，如果您在几个地方使用它并希望其在基础数据发生更改时自动更新，则这将是有益的。

> 现在需要`EcsRx.Plugins.Computeds` nuget包，因为已将计算的功能移到了那里。

## Querys

因此，到目前为止，我们已经讨论了一般的筛选过程，但是有一些扩展方法可以让您对数据进行更多的即席查询，这些查询不会以任何方式进行缓存，但可以让您深入了解数据的子集。这是一种预定义的方法，与“ IComputedGroup”有点重叠，但可以直接在池级别或访问者级别进行查询。

IEntityCollection和IbservableGroup都具有Query扩展方法，该方法采用IEntityCollectionQuery或IbservableGroupQuery，您可以在其中实现所需的查询逻辑。这样做是为了使您可以更像存储库一样使用池和组访问器，并使用预定义的查询来一致地访问它们。
