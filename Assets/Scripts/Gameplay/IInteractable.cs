using UnityEngine;

namespace AirportSurvival.Gameplay
{
    public interface IInteractable
    {
        string InteractionPrompt { get; }
        bool CanInteract { get; }
        Transform Transform { get; }
        void Interact(PlayerInteractor interactor);
    }
}
