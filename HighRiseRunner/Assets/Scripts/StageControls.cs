using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using System.Text.RegularExpressions;

public class StageControls : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject fadeIn;
    [SerializeField] GameObject stageDistance;
    [SerializeField] List<GameObject> levels = new List<GameObject>();
    [SerializeField] int selectedLevelIndex = 0;
    [SerializeField] Transform[] camLevelPositions;
    public float slideTime = 2f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] CanvasGroup stageScreen;
    [SerializeField] TextMeshProUGUI stageTitle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageTitle.text = Regex.Replace(levels[selectedLevelIndex].name, "(\\B[A-Z])", " $1");
        stageDistance.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + PlayerPrefs.GetInt(levels[selectedLevelIndex].name + "HighScore");
        StartCoroutine(FadeInTurnOff());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressPlay()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1);
        LoadToScript.chosenLevelName = levels[selectedLevelIndex].name;
        SceneManager.LoadScene(3);
    }
    IEnumerator FadeInTurnOff()
    {
        yield return new WaitForSeconds(1);
        fadeIn.SetActive(false);

    }

    public void PressNext()
    {
        if (selectedLevelIndex >= camLevelPositions.Length - 1) return;

        selectedLevelIndex++;
        SlideToCurrent();
    }

    public void PressPrevious()
    {
        if (selectedLevelIndex <= 0) return;

        selectedLevelIndex--;
        SlideToCurrent();
    }

    void SlideToCurrent()
    {
        cameraTransform
            .DOMove(camLevelPositions[selectedLevelIndex].position, slideTime)
            .SetEase(Ease.InOutCubic);

        UpdateUI();
    }

    void UpdateUI()
    {
        stageScreen.DOFade(0, 1f).OnComplete(() =>
        {
            stageTitle.text = Regex.Replace(levels[selectedLevelIndex].name, "(\\B[A-Z])", " $1");
            stageDistance.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + PlayerPrefs.GetInt(levels[selectedLevelIndex].name + "HighScore");
            stageScreen.DOFade(1, 1f);
        });
    }
}
