# Infrastructure

作为EcsRx的一部分，为您提供了一些基本基础结构（如果您选择使用`EcsRx.Infrastructure`），则包含：

- 依赖项注入抽象系统（因此，您可以在具有任何DI框架的任何平台上使用DI）
- 一个“ IEcsRxApplication”接口以及一个默认的实现“ EcsRxApplication”（因此您可以以一致的方式启动您的应用程序）
- 通过`IEcsRxPlugin`的插件框架（因此您可以编写自己的插件，这些插件可以在许多项目中重复使用并与他人共享）
- 默认的“ EventSystem”（因此，您可以在实现“ IEventSystem”的应用程序周围发送事件）

所有这些结合起来基本上为您提供了开始创建应用程序的入口。

## Why use this?

为了具有某种一致性和可扩展性，例如，通过添加基础结构，您可以立即使用任何EcsRx插件（假设它们不包含任何本地平台代码），也可以使用特定的生存期方法和约定。

如果您有特定的情况，并且不想使用内置的基础结构，则可以轻松地忽略它并放置您自己的东西，但这将意味着您与许多具有一致性和兼容性的好东西不兼容。社区都遵守这些合同。
