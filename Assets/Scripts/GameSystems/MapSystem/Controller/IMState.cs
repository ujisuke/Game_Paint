using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public interface IMState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
