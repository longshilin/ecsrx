# Stuff To Know

在我们开始之前，应该有一些现有的知识，如果您不知道下面的内容，那么请继续阅读（每个标题的链接，您都可以单击以了解它）。您不需要了解所有这一切，但这将非常有益。

## [ECS Pattern](https://grofit.gitbook.io/development-for-winners/development/game-dev/patterns/ecs)

这涵盖了什么是ECS，以及组件、实体和系统如何在ECS系统中交互，而不是编写另一套“什么是ECS”文档，如果你不知道ECS是什么，只需阅读上面的链接或用谷歌搜索主题就更容易了。

## [Inversion Of Control](https://grofit.gitbook.io/development-for-winners/development/general/dependency-patterns/inversion-of-control)

这是关于对象（通常是类）如何获得依赖关系的，这种方法在整个代码库中大量使用，意味着类所需的大部分内容都是通过其构造函数传入的。

## [Dependency Injection](https://grofit.gitbook.io/development-for-winners/development/general/dependency-patterns/dependency-injection)

这与IoC有关，基本上是一种以灵活且高度可配置的方式解决对象依赖关系的行业标准方法。如果您曾经看到过任何具有类似`Bind<ISomething>().To<Something>()`这样位的代码，那么这就是DI配置。

## [Reactive Extensions (RX)](https://grofit.gitbook.io/development-for-winners/development/general/data-patterns/reactive-extensions)

这个库试图利用常见的ECS范式，同时将反应性作为它的一大优势。如果你不确定rx是什么，或者它对你的项目有什么好处，那么就读懂它，简单地说它是一种数据改变的推送方法，而不是轮询方法。

## [Intro to Unit Testing](https://grofit.gitbook.io/development-for-winners/development/general/testing/intro-to-testing)

如果你没有做过任何单元测试，那就不是世界末日了，你不需要突然成为一个测试rockstar，但是使用这个框架的一部分好处是它可以让你的代码保持高度的可测试性，所以如果你想利用这个好处，那么知道如何使用它是值得的。

## [Mocking in Unit Tests](https://grofit.gitbook.io/development-for-winners/development/general/testing/mocking)

与单元测试相关，它介绍了如何提供依赖项的 mocked/fake 实现，以强制代码以某种方式运行，然后确认它的行为符合预期。

## 如果我还需要帮助或有问题怎么办！？？！

如果您想了解以上主题的更多信息，请随时访问我们的[Discord频道](https://discord.gg/bS2rnGz)进一步讨论或提出任何问题。