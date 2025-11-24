using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToScript : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
