using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2;
    public float horizontalSpeed = 3;

    InputAction moveAction;

    private void Start()
    {
        this.moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveInput = this.moveAction.ReadValue<Vector2>();

        Vector3 translation = Vector3.forward * moveInput.y * Time.deltaTime * playerSpeed + Vector3.right * moveInput.x * Time.deltaTime * horizontalSpeed;

        transform.Translate(translation);
    }
}
