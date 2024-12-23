using System.Collections;
using UnityEngine;
using VG;
using Zenject;

namespace VG2
{
    public class ProjectInstaller : Installer
    {
        public bool Installed { get; private set; } = false;

        public override void InstallBindings()
        {
            var gameState = Saves.InitializeGameState();
            StaticContext.Container.Bind<GameState>().FromInstance(gameState).AsSingle();

        }



    }

}

