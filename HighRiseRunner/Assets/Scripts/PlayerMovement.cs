using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10;
    public float horizontalSpeed = 4;

    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    InputAction moveAction;

    [SerializeField] bool isRunning;

    private void Start()
    {
        this.moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning)
        {
            isRunning = true;
            StartCoroutine(AddDistance());
        }

        Vector2 moveInput = this.moveAction.ReadValue<Vector2>();

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);

        transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * horizontalSpeed);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
        transform.position = pos;
    }

    IEnumerator AddDistance()
    {
        yield return new WaitForSeconds(0.35f);
        MasterInfo.distanceRun++;
        isRunning = false;
    }
}
