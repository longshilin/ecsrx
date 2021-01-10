# Setup

如果您使用的是Monogame或Unity，那么您可以直接获得特定于平台的帮助程序，这些帮助程序将为您提供启动所需的大部分基础设施和设置，所以请查看那里。

---

如果您不使用Unity或Monogame，并且希望开拓一个新领域，那么这只是设置系统核心部分运行所需部分的示例。

## 是否预建基础设施？

开箱即用的EcsRx附带了大量简化安装的基础结构类，如果您愿意使用这些类作为安装的基础，那么您的工作就会变得简单一些。

这些文档中还有一个完整的部分，介绍了基础设施以及如何在常见场景中使用应用程序和其中的其他类。

### 是的，我要所有的基础设施

一个明智的选择，因此建议您从以下几点开始：

- `EcsRx`
- `EcsRx.Infrastructure`
- `EcsRx.Plugins.ReactiveSystems`
- `EcsRx.Plugins.Views`

这将为您提供扩展的基本类，但是一个基本问题是，基础结构希望您使用DI框架。它实际上并不关心哪个DI框架，因为它提供了一个接口供您实现，然后在您自己的 `EcsRxApplication` 实现中使用。

所以这里是你需要的主要部分:

- 为您选择的DI框架实现 `IDependencyContainer` 。 [这是一个例子](https://github.com/EcsRx/ecsrx/blob/master/src/EcsRx.Infrastructure.Ninject/NinjectDependencyContainer.cs)

- 实现您自己的 `EcsRxApplication` 类，给它一个 `IDependencyContainer` 实现来使用。[这是一个例子](https://github.com/EcsRx/ecsrx/blob/master/src/EcsRx.Examples/Application/EcsRxConsoleApplication.cs)

- 为需要创建的每个逻辑应用程序扩展自定义的 `EcsRxApplication` 实现，如EcsRx控制台示例所示

**Ninject** 和 **Zenject** 都有预先制作的DI实现，所以如果你能在你的平台上使用其中一个，那就太好了！如果没有，那么就选择一个DI框架并为它实现自己的处理程序（以ninject为例）。

> 所以如果你不知道依赖注入是什么，我建议你去[读这个](https://grofit.gitbooks.io/development-for-winners/content/development/general/dependency-patterns/dependency-injection.html) 和 [这个](https://grofit.gitbooks.io/development-for-winners/content/development/general/dependency-patterns/inversion-of-control.html) 这将使您快速了解什么是IoC（控制反转）和DI，以及如何使用它。

这里值得注意的是，这正是本项目中示例的工作方式，因此打开这些示例以了解它们是如何完成的是值得的，但是相同的原则可以应用到您自己的应用程序中。

### 不！我不喜欢你的设计模式，先生，让我走吧

好的，队长，如果你只想用最小的努力来完成任务，那么我只需要得到核心库，并手动实例化所有需要的东西。

这就像我建议的最简单的设置：

```csharp
public abstract class EcsRxApplication
{
	public ISystemExecutor SystemExecutor { get; }
	public IEventSystem EventSystem { get; }
	public IObservableGroupManager ObservableGroupManager { get; }
	public IEntityDatabase EntityDatabase { get; }

	protected EcsRxApplication()
	{
		// For sending events around
		EventSystem = new EventSystem(new MessageBroker());
		
		// For mapping component types to underlying indexes
		var componentTypeAssigner = new DefaultComponentTypeAssigner();
		var allComponents = componentTypeAssigner.GenerateComponentLookups();
		
		var componentLookup = new ComponentTypeLookup(allComponents);
		// For interacting with the component databases
		var componentDatabase = new ComponentDatabase(componentLookup);
		var componentRepository = new ComponentRepository(componentLookup, componentDatabase);	
		
		// For creating entities, collections, observable groups and managing Ids
		var entityFactory = new DefaultEntityFactory(new IdPool(), componentRepository);
		var entityCollectionFactory = new DefaultEntityCollectionFactory(entityFactory);
		EntityDatabase = new EntityDatabase(entityFactory);
		var observableGroupFactory = new DefaultObservableObservableGroupFactory();
		ObservableGroupManager = new EntityCollectionManager(observableGroupFactory, entityDatabase, componentLookup);

		// All system handlers for the system types you want to support
		var manualSystemHandler = new ManualSystemHandler(ObservableGroupManager);
		var basicSystemHandler = new BasicSystemHandler(ObservableGroupManager);

		var conventionalSystems = new List<IConventionalSystemHandler>
		{
			manualSystemHandler,
            basicSystemHandler
		};
		
		// The main executor which manages how systems are given information
		SystemExecutor = new SystemExecutor(conventionalSystems);
	}

	public abstract void StartApplication();
}
```

那你要做的就是下面这样:

```csharp
public class HelloWorldExampleApplication : EcsRxApplication
{
	public override void StartApplication()
	{
		SystemExecutor.AddSystem(new TalkingSystem());

		var defaultCollection = EntityCollectionManager.GetCollection();
		var entity = defaultCollection.CreateEntity();

		var canTalkComponent = new CanTalkComponent {Message = "Hello world"};
		entity.AddComponent(canTalkComponent);
	}
}
```

HUZZAH！现在，您已经开始运行了，您可以围绕此创建自己的约定或设计模式。