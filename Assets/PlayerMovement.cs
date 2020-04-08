using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 movementDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (GameModeSwitch.IsHumanoidControlled)
        {
            if (characterController.isGrounded)
            {
                movementDirection = transform.forward * -Input.GetAxis("Vertical") + transform.right * -Input.GetAxis("Horizontal");
                movementDirection *= speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    movementDirection.y = jumpSpeed;
                }
            }

            movementDirection.y -= gravity * Time.deltaTime;

            characterController.Move(movementDirection * Time.deltaTime);
        }
    }
}
