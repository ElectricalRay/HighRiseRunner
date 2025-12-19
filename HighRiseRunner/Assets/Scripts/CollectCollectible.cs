using UnityEngine;

public abstract class CollectCollectible : MonoBehaviour
{
    public AudioSource sfx;

    protected virtual void OnCollect() { }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            sfx.Play();
            OnCollect();
            this.gameObject.SetActive(false);
        }
    }
}
