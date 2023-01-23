using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.States;
using Game.Scripts.Logic;
using Game.Scripts.Services.Input;

namespace Game.Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}