using UnityEngine;

namespace AirportSurvival.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class SimplePlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float gravity = -18f;

        private CharacterController controller;
        private Vector3 verticalVelocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            input = Vector3.ClampMagnitude(input, 1f);

            Vector3 move = transform.TransformDirection(input) * moveSpeed;
            if (controller.isGrounded && verticalVelocity.y < 0f)
            {
                verticalVelocity.y = -1f;
            }

            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move((move + verticalVelocity) * Time.deltaTime);
        }
    }
}
