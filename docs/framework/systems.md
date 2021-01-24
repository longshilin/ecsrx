# Systems

系统是所有逻辑生存的地方，它从集合中获取实体并在每个逻辑上执行逻辑。系统的设计方式是一个业务流程层，该流程层包装了所有系统并处理池之间的通信以及称为` ISystemExecutor`的 execution/reaction/setup 方法（可以在其他页面上找到）。

这意味着您的系统无需担心获取实体和与实体打交道的后勤问题，只需表达您想与实体进行交互的方式，并让SystemExecutor处理繁重的工作并将实体传递给您进行处理。当您查看所有可用的系统接口，这些接口都处理单个实体而不是它们的组时，很容易看出这一点。

## System Types

这是很有趣的地方，因此我们有多种类型的系统，这取决于您要使用实体的方式，默认情况下有` IManualSystem`，但是有一个项目包含所有最常见的系统（` EcsRx.Systems`）。您也可以将它们混合在一起，这样就可以有一个系统实现` ISetupSystem`，` ITeardown`和` IReactToEntitySystem`，当它们加入组时，它们将为每个实体运行设置方法，然后对实体更改做出反应并根据更改进行处理，然后在将实体从组中删除时最终运行一些逻辑。

所有系统都具有“组”的概念，该概念描述了要从池中定位的实体，因此，除了设置正确的分组并为接口实现方法之外，您无需做其他事情。

### IManualSystem

当您想在实体范围之外执行某些逻辑，或者想要对如何处理匹配的实体进行更精细的控制时，这是一个利基系统。

而不是由SystemExecutor为您完成大部分工作并管理订阅和实体交互，这只是为您提供了针对目标实体的GroupAccessor，并由您来控制它们的处理方式。

将系统添加到执行程序时，将触发“ StartSystem”方法，而在删除系统时将触发“ StopSystem”。

## System Loading Order

因此，默认情况下（使用ISystemExecutor的默认实现），系统将按以下顺序加载：
1. Implementations of `ISetupSystem`
2. Implementations of `IReactToEntitySystem`
3. Other Systems

但是，在这些分组中，它将按照 Zenject/Extenject（假定您正在使用）提供的顺序加载系统，但是有一种方法可以通过应用[Priority（1）]属性来强制执行某些级别的优先级，这使您可以指定应如何加载系统的优先级。顺序从最低到最高，因此如果优先级为1，它将在优先级为10的系统之前加载。
