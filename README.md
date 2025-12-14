TODO

Welcome to Tanuki, my work in progress personal project

Right now it is split into 2 sections: infrastructure and game

# Infrastructure
Group of minimalistic general purpose frameworks and utilities I have developed

It contains all the code that could be shared between games and the idea is to reuse it to implement several completely different games. Infrastructure code is placed in main and dev branches while game code is placed in its own branch. Ideally infrastructure could have its own repository and be used by several game repositories (by using it as a package, subtree, submodule, etc), and this is something I may consider doing in the future, but for now this very simple branch approach is what I am using

Infrastructure is being updated and extended as game needs change. This is why some of its parts are not finished yet

Finally, most of its code is tested (around 550 tests)

It contains
- Configuring: Define configs for the game or even for infrastructure itself
- DependencyInjection: Inject dependencies to all infrastructure and game services. It also works for any class with managed constructor (like MonoBehaviour, etc). TODO: Add its own readme file with more information and examples
- Gating
- Logging
- ModelViewViewModel
- ScreenLoading
- System
- Tweening
- Unity
- UnityUtils

# Game
