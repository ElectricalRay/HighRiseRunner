using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class BasicCharacterMovementScript : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;

    public float moveSpeed = 5.0f;
    public float jumpStrength = 1.0f;
    public float mass = 1.0f;
    public float maximumGroundDistance = 2.0f;

    public LayerMask groundLayers;

    public Vector3 forward;
    public Vector3 right;

    public Rigidbody rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.moveAction = InputSystem.actions.FindAction("Move");
        this.jumpAction = InputSystem.actions.FindAction("Jump");

        this.forward = Vector3.forward;
        this.right = Vector3.right;

        this.rigidbody.mass = mass;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = this.moveAction.ReadValue<Vector2>();

        Vector3 translation = forward * moveInput.y + right * moveInput.x;

        this.transform.position += translation * moveSpeed * Time.deltaTime;

        float jumpInput = this.jumpAction.ReadValue<float>();

        if(jumpInput == 1)
        {
            if(Physics.Raycast(this.transform.position, Vector3.down, this.maximumGroundDistance, this.groundLayers))
            {
                this.rigidbody.AddForce(new Vector3(0, this.jumpStrength, 0), ForceMode.Impulse);
            }
        }
    }
}
