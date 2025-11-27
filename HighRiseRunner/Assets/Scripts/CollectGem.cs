using UnityEngine;

public class CollectGem : MonoBehaviour
{
    [SerializeField] AudioSource gemFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gemFX.Play();
            MasterInfo.gemCount++;
            this.gameObject.SetActive(false);
        }
    }
}
