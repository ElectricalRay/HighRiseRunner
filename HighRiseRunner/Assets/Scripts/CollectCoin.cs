using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource coinFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            coinFX.Play();
            MasterInfo.coinCount++;
            this.gameObject.SetActive(false);
        }
    }
}
