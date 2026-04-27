using UnityEngine;

namespace AirportSurvival.Gameplay
{
    public sealed class TrashItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private AirportGameController gameController;
        [SerializeField] private float secondsBeforeScolding = 12f;
        [SerializeField] private string neglectReason = "쓰레기를 안 치웠잖아";

        private float age;
        private bool cleaned;
        private bool managerAlreadyScolded;

        public string InteractionPrompt => "Press E to clean trash";
        public bool CanInteract => !cleaned;
        public Transform Transform => transform;

        private void Awake()
        {
            if (gameController == null)
            {
                gameController = FindObjectOfType<AirportGameController>();
            }
        }

        private void Update()
        {
            if (cleaned || managerAlreadyScolded || gameController == null || gameController.IsDayEnded)
            {
                return;
            }

            age += Time.deltaTime;
            if (age >= secondsBeforeScolding)
            {
                managerAlreadyScolded = true;
                gameController.TriggerManagerScolding(neglectReason);
            }
        }

        public void Interact(PlayerInteractor interactor)
        {
            if (!CanInteract)
            {
                return;
            }

            cleaned = true;
            if (gameController != null)
            {
                gameController.RegisterTrashCleaned();
            }

            gameObject.SetActive(false);
        }
    }
}
