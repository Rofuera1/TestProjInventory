using UnityEngine;
using Zenject;

namespace ItemLibraryModule
{
    public class LibraryInstaller : MonoInstaller
    {
        [SerializeField] private LibraryScriptable _libraryScriptable;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ItemLibrarySystem>().AsSingle().WithArguments(_libraryScriptable);
        }
    }
}