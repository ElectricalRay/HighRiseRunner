using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10;
    public float horizontalSpeed = 4;

    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    InputAction moveAction;

    private void Start()
    {
        this.moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = this.moveAction.ReadValue<Vector2>();

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);

        transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * horizontalSpeed);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
        transform.position = pos;
    }
}
