using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;

    public float playerSpeed = 10;
    public float speedIncreasePerSec = 0.05f;
    public float maxSpeed = 50f;
    public float horizontalSpeed = 4;
    public float jumpForce = 5;

    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    public bool isJumping;
    public bool onGround;
    public bool isSliding;
    public bool canMoveSide = true;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction slideAction;

    [SerializeField] bool isRunning;

    public Rigidbody rb;

    [SerializeField] List<GameObject> characters = new List<GameObject>();
    public GameObject playerAnimator;
    public int selectedCharacter;

    [SerializeField] GameObject triggerBox;

    Vector3 triggerBoxDefaultPos;
    Quaternion triggerBoxDefaultRot;

    private void Awake()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        playerAnimator = characters[selectedCharacter];

        playerAnimator.SetActive(true);
    }

    private void Start()
    {;

        this.moveAction = InputSystem.actions.FindAction("Move");
        this.jumpAction = InputSystem.actions.FindAction("Jump");
        this.slideAction = InputSystem.actions.FindAction("Sprint");

        this.jumpAction.started += onJumpStarted;
        this.slideAction.started += onSlideStarted;
        this.moveAction.started += onMoveSideStart;
        this.moveAction.canceled += onMoveSideEnd;

        triggerBoxDefaultPos = triggerBox.transform.localPosition;
        triggerBoxDefaultRot = triggerBox.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed += speedIncreasePerSec * Time.deltaTime;
        playerSpeed = Mathf.Clamp(playerSpeed, 0, maxSpeed);

        if(!canMove)
        {
            this.jumpAction.started -= onJumpStarted;
            this.slideAction.started -= onSlideStarted;
            this.moveAction.started -= onMoveSideStart;
            this.moveAction.canceled -= onMoveSideEnd;
            return;
        }

        if(!isRunning)
        {
            isRunning = true;
            StartCoroutine(AddDistance());
        }

        Vector2 moveInput = this.moveAction.ReadValue<Vector2>();

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);

        if(canMoveSide)
        {
            transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * horizontalSpeed);
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
        transform.position = pos;
    }

    IEnumerator AddDistance()
    {
        yield return new WaitForSeconds(15 / playerSpeed);
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
        if(onGround && !isSliding)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            this.horizontalSpeed = 2;
            playerAnimator.GetComponent<Animator>().SetBool("isGrounded", false);
            playerAnimator.GetComponent<Animator>().Play("Jump Start");
        }
    }

    public void onSlideStarted(InputAction.CallbackContext context)
    {
        if(onGround && !isSliding)
        {
            playerAnimator.GetComponent<Animator>().Play("Slide");
            Vector3 triggerPos = triggerBox.transform.localPosition;
            triggerPos.y = (float)-0.7;
            triggerBox.transform.localPosition = triggerPos;

            triggerBox.transform.localRotation = Quaternion.Euler(90, 0, 0);
            isSliding = true;
            canMoveSide = false;

            StartCoroutine(ResetTriggerAfterSlide());
        }
    }

    IEnumerator ResetTriggerAfterSlide()
    {
        float slideTime = playerAnimator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(slideTime + (float)0.75);

        triggerBox.transform.localPosition = triggerBoxDefaultPos;
        triggerBox.transform.localRotation = triggerBoxDefaultRot;

        isSliding = false;
        canMoveSide = true;
    }

    public void onMoveSideStart(InputAction.CallbackContext context) 
    {
        Vector2 sideInput = context.ReadValue<Vector2>();
        if(sideInput.x > 0)
        {
            playerAnimator.GetComponent<Animator>().SetBool("isRunRight", true);
        } else if (sideInput.x < 0)
        {
            playerAnimator.GetComponent<Animator>().SetBool("isRunLeft", true);
        }
    }

    public void onMoveSideEnd(InputAction.CallbackContext context)
    {
        playerAnimator.GetComponent<Animator>().SetBool("isRunRight", false);
        playerAnimator.GetComponent<Animator>().SetBool("isRunLeft", false);
    }
}
