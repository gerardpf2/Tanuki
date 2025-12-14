# Infrastructure

Welcome to <b>Tanuki infrastructure</b>

Group of minimalistic general purpose utilities

It contains code that could be shared between games and the idea is to reuse it to implement multiple completely different games. Infrastructure code is placed in <b>main</b> branch while game code is placed in its own branch. Ideally infrastructure could have its own repository and be used by multiple game repositories (by using it as a package, subtree, submodule, etc), and this is something I may consider doing in the future, but for now this very simple branch approach is what I am using

Infrastructure is being updated and extended as game needs change. This is why some of its parts are not finished yet

Finally, most of its code is tested (around 550 tests)

It contains
- <b>Configuring</b>: Define configs for the game or even for infrastructure itself
- <b>DependencyInjection</b>: My own dependency injection framework! Inject dependencies to all infrastructure and game services. Classes with managed constructor (like MonoBehaviour, etc) can use it too. Click [here](https://github.com/gerardpf2/Tanuki/tree/main/Assets/Scripts/Infrastructure/DependencyInjection) for more info
- <b>Gating</b>: Gate features by config or by version
- <b>Logging</b>: Log against multiple handlers (one of them being Unity's logger)
- <b>ModelViewViewModel</b>: My own Model-View-ViewModel framework! Use view models, bound properties, methods and triggers, etc to connect model and view. No reflection involved. Click [here](https://github.com/gerardpf2/Tanuki/tree/main/Assets/Scripts/Infrastructure/ModelViewViewModel) for more info
- <b>ScreenLoading</b>: Register and load screens (prefabs) to specific placements
- <b>System</b>: General purpose utilities (exception, collection, parsing, etc)
- <b>Tweening</b>: My own tweening framework! Define and run tweens (and sync / async sequences) to update game entities. Click [here](https://github.com/gerardpf2/Tanuki/tree/main/Assets/Scripts/Infrastructure/Tweening) for more info
- <b>Unity</b>: General purpose Unity utilities (animator, pooling, etc)
