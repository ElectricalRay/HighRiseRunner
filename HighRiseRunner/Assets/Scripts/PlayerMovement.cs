using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;

    public float playerSpeed = 10;
    public float horizontalSpeed = 4;
    public float jumpForce = 5;

    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    public bool isJumping;
    public bool onGround;

    InputAction moveAction;
    InputAction jumpAction;

    [SerializeField] bool isRunning;

    public Rigidbody rb;

    [SerializeField] GameObject playerAnimator;

    private void Start()
    {
        this.moveAction = InputSystem.actions.FindAction("Move");
        this.jumpAction = InputSystem.actions.FindAction("Jump");

        this.jumpAction.started += onJumpStarted;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove)
        {
            this.jumpAction.started -= onJumpStarted;
            return;
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            onGround = true;
            this.horizontalSpeed = 6;
            playerAnimator.GetComponent<Animator>().SetBool("isGrounded", true);
        }
    }

    public void onJumpStarted(InputAction.CallbackContext context)
    {
        if(onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            this.horizontalSpeed = 2;
            playerAnimator.GetComponent<Animator>().SetBool("isGrounded", false);
            playerAnimator.GetComponent<Animator>().Play("Jump Start");
        }
    }
}
