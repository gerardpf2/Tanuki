# Dependency injection

Welcome to <b>Tanuki dependency injection framework</b>

My own dependency injection framework! Inject dependencies to all infrastructure and game services. Classes with managed constructor (like MonoBehaviour, etc) can use it too. No reflection involved

### Key concepts
- <b>ScopeComposer</b>: Defines how a scope is going to be composed by adding its rules and initializing its services. It can have partial and child scope composers
- <b>Rule</b>: Defines how a service is going to be resolved. There are different types of rules
  - <b>InstanceRule\<T></b>: Returns the provided already constructed instance of type T
  - <b>TransientRule\<T></b>: Constructs and returns a different instance of type T
  - <b>SingletonRule\<T></b>: Constructs and returns the same instance of type T
  - <b>ToRule\<TInput, TOutput></b>: Resolves TOutput and returns its result
  - <b>GateKeyRule\<T></b>: Resolves T and returns its result only if the provided gate key is satisfied. Gating is usually managed at scope composer level, but it can also be managed at rule level
  - <b>TargetRule\<T></b>: Resolves T using the rule resolver of another scope. This means, with a different visibility over rules. This is used when adding shared rules (they target the shared rule resolver)
  - <b>InjectRule\<T></b>: Resolves T in order to execute one of its methods. This is used when injecting services to classes with managed constructor
- <b>ScopeBuilder</b>: Uses a scope composer to build a scope (in a recursive way)
- <b>Scope</b>: Represents a feature, game mode, etc and contains its rules. It can also have partial and child scopes
- <b>ScopeInitializer</b>: Initializes the services of a scope (in a recursive way)

Any feature, game mode, etc can be easily integrated to Tanuki composition by defining its scope composer

### How does rule visibility work?

When resolving T, the rule resolver is going to try to find a rule of T on its scope rules container. If it cannot be found, try it targeting the parent of that scope (in a recursive way)

For example
1) There are 3 scopes, SA, SB and SC
2) SB is the child of SA, and SC is the child of SB
3) SA can see SA rules
4) SB can see SB and SA rules
5) SC can see SC, SB and S rules

### What is a partial scope?

It can be seen as a scope that is split into multiple scopes, all them having the same visibility over rules

For example
1) Scope S contains rules that have to do with features FA and FB
2) Then, scopes SA and SB can be created
3) But FA services have FB services as dependencies, and vice versa
4) This means that SA cannot be child of S or SB. The same happens for SB
5) Then, the best option is to set SA and SB as partials of S
6) Finally, 3 scope composers are going to be used instead of 1 to integrate this to Tanuki composition, and the scope builder is going to build the 3 scopes described in here

Is using partial scopes a need? No, it is not. But it can be useful to keep scope composers small and organized when they behave like the ones described at point 3)

### Example
Lets assume that these are the services of the feature, game mode, etc
```csharp
public interface IServiceA { }
    
public class ServiceA : IServiceA { }

public interface IServiceB
{
    void Initialize();
}

public class ServiceB : IServiceB
{
    public ServiceB(IServiceA serviceA) { }
    
    public void Initialize() { }
}
```
Then, it can be defined like this
```csharp
public class MyComposer : ScopeComposer
{
    protected override void AddRules(IRuleAdder ruleAdder, IRuleFactory ruleFactory)
    {
        ruleAdder.Add(
            ruleFactory.GetSingleton<IServiceA>(_ =>
                new ServiceA()
            )
        );

        ruleAdder.Add(
            ruleFactory.GetSingleton<IServiceB>(r =>
                new ServiceB(
                    r.Resolve<IServiceA>()
                )
            )
        );
    }

    protected override void Initialize(IRuleResolver ruleResolver)
    {
        ruleResolver.Resolve<IServiceB>().Initialize();
    }
}
```
In here, it can be seen that
1) It inherits from <b>ScopeComposer</b>
2) <b>AddRules</b> method is overriden because there are services to add
3) <b>Initialize</b> method is overriden because there are services to initialize

Finally, it needs to be set as partial or child of another one
```csharp
public class AnotherComposer : ScopeComposer
{
    protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
    {
        return base.GetChildScopeComposers().Append(new MyComposer());
    }
}
```
<b>RootComposer</b>, as it name suggests, is the root of Tanuki composition

### More examples?

Most infrastructure and game sections have a folder called <b>Composition</b> with a scope composer inside. Any of them can work as an example
