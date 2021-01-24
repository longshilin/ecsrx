# Application Lifecycle

该库的基础结构方面提供了默认应用程序类，该类应根据需要从其继承或重写。这提供了一种使应用程序保持一致并确保按预期加载的方式。这是默认的生命周期方法，以及它们何时在应用程序启动过程中运行。

1. `LoadModules`
这是您应该加载自己的模块的地方，base.LoadModules（）将加载默认框架，因此，如果您不希望这样做并且想要加载自己的优化框架组件，则不要调用基本版本。优化性能测试中显示了一个示例，在该测试中我们手动分配了组件类型ID，因此我们不希望使用默认加载器。

2. `LoadPlugins`
在这里，您应该加载要使用的所有插件，如果没有要使用的插件，那么就不必重写它。

3. `ResolveApplicationDependencies`
这是从DI容器手动解析应用程序的依赖关系的地方，因此，一旦所有插件和模块都运行，此时ISystemExecutor和IEventSystem等将全部解析。 base.ResolveApplicationDependencies（）将设置核心EcsRxApplication依赖关系，因此您应该先调用此依赖项，然后在此之后解决所需的任何特定问题。

4. `BindSystems`
这是所有系统绑定的地方（这意味着它们在DI容器中，但未解析），默认情况下，它将自动绑定应用程序范围内的所有系统（使用BindAllSystemsWithinApplicationScope），但是如果不这样做，则可以覆盖和删除基本调用想要这种行为，或者如果要手动注册其他系统，可以让它自动注册应用程序范围内的系统，然后手动绑定所需的任何其他系统。

5. `StartSystems`
这是应该启动所有绑定系统的位置（它们已添加到ISystemExecutor），默认情况下，此阶段会将所有绑定系统添加到活动系统执行程序（使用StartAllBoundSystems），但是您可以覆盖此行为以手动控制哪些系统将被启动，但是在大多数情况下，此默认行为将满足您的要求。

6. `ApplicationStarted`
此时，您需要准备的一切都已准备就绪，可以开始创建集合，实体并开始游戏。