# Blueprints

蓝图提供了一种重新使用的方式来设置具有预定义组件和值的实体，最终取决于开发人员他们希望如何实现蓝图，因为您可以根据需要向其添加尽可能多的可配置属性，但是它们都可以实现适用于实体的`Apply`方法。

Here is an example of a blueprint:

```csharp
public class PlayerBlueprint : IBlueprint
{
	public string Name {get;set;}
	public string Class {get;set;}
	public int Health {get;set;}
	
	public void Apply(IEntity entity)
	{
		entity.AddComponent(new HasNameComponent{ Name = Name });
		entity.AddComponent(new HasClassComponent{ Class = Class });
		entity.AddComponent(new HasHealthComponent{ MaxHealth = Health, CurrentHealth = Health });
	}
}
```

## Creating via blueprints

池知道蓝图，您可以使用蓝图创建实体，以节省创建实体然后手动应用所有组件的时间，如下所示：

```csharp
var hanSoloEntity = somePool.createEntity(new PlayerBlueprint{ 
	Name = "Han Solo", 
	Class = "Smuggler", 
	Health = 100});
```

## Applying blueprints to existing entities

您有两种将蓝图应用于实体的选择，一种是只是将所需的蓝图更新并调用传入实体中的` Apply`方法，也可以使用可用的扩展方法直接从实体中应用，这也是可链接的，因此您可以根据需要将多个蓝图应用于同一实体，例如：

```csharp
entity.ApplyBlueprint(new DefaultActorBlueprint())
	.ApplyBlueprint(new DefaultEquipmentBlueprint())
	.ApplyBlueprint(new SetupNetworkingBlueprint());
```

## Blurb

通常，您只需要使用一个蓝图来设置对象，当前只能在代码中完成，但是现在有了视图的概念，应该可以以某种有意义的方式将蓝图公开给编辑器。
