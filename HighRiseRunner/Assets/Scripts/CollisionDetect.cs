using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject playerAnimator;
    [SerializeField] AudioSource collisionFX;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject fadeOut;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CollisionEnd());
    }

    IEnumerator CollisionEnd()
    {
        collisionFX.Play();
        thePlayer.GetComponent<PlayerMovement>().canMove = false;

        Rigidbody playerRb = thePlayer.GetComponent<Rigidbody>();
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        playerRb.useGravity = true;

        playerAnimator.GetComponent<Animator>().Play("Stumble Backwards");
        mainCamera.GetComponent<Animator>().Play("CollisionCamera");

        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
