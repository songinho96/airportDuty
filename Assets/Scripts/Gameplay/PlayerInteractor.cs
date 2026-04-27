using UnityEngine;

namespace AirportSurvival.Gameplay
{
    public sealed class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float interactionRange = 2f;
        [SerializeField] private LayerMask interactableLayers = ~0;

        private IInteractable focusedInteractable;

        public string CurrentPrompt => focusedInteractable != null && focusedInteractable.CanInteract
            ? focusedInteractable.InteractionPrompt
            : string.Empty;

        private void Update()
        {
            focusedInteractable = FindNearestInteractable();
            if (focusedInteractable != null && focusedInteractable.CanInteract && Input.GetKeyDown(KeyCode.E))
            {
                focusedInteractable.Interact(this);
            }
        }

        private IInteractable FindNearestInteractable()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, interactableLayers);
            IInteractable nearest = null;
            float nearestDistance = float.MaxValue;

            foreach (Collider hit in hits)
            {
                IInteractable interactable = hit.GetComponentInParent<IInteractable>();
                if (interactable == null || !interactable.CanInteract)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, interactable.Transform.position);
                if (distance < nearestDistance)
                {
                    nearest = interactable;
                    nearestDistance = distance;
                }
            }

            return nearest;
        }
    }
}
