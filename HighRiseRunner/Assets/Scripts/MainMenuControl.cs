using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject bounceText;
    [SerializeField] GameObject bigButton;
    [SerializeField] GameObject animCam;
    [SerializeField] GameObject staticCam;
    [SerializeField] GameObject menuControls;
    [SerializeField] AudioSource buttonSelect;
    [SerializeField] GameObject fadeIn;
    [SerializeField] GameObject gems;
    [SerializeField] GameObject coins;
    public static bool hasClicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + PlayerPrefs.GetInt("TotalCoins");
        gems.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + PlayerPrefs.GetInt("TotalGems");


        StartCoroutine(FadeInTurnOff());
        if (hasClicked)
        {
            staticCam.SetActive(true);
            animCam.SetActive(false);
            menuControls.SetActive(true);
            bounceText.SetActive(false);
            bigButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuBeginButton()
    {
        StartCoroutine(AnimCam());
    }

    public void StartGame()
    {
        MasterInfo.gemCount = 0;
        MasterInfo.coinCount = 0;
        MasterInfo.distanceRun = 0;
        StartCoroutine(StartButton());
    }

    IEnumerator StartButton()
    {
        buttonSelect.Play();
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    IEnumerator AnimCam()
    {
        animCam.GetComponent<Animator>().Play("AnimMenuCamera");
        bounceText.SetActive(false);
        bigButton.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        fadeIn.SetActive(false);
        staticCam.SetActive(true);
        animCam.SetActive(false);
        menuControls.SetActive(true);
        hasClicked = true;
    }
    
    IEnumerator FadeInTurnOff()
    {
        yield return new WaitForSeconds(1);
        fadeIn.SetActive(false);
    }
}
