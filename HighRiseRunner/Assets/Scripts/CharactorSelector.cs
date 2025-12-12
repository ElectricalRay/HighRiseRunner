using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharactorSelector : MonoBehaviour
{
    public GameObject[] Characters;
    public int number;
    int selectedChar;
    [SerializeField] TextMeshProUGUI SelectButtonText;
    [SerializeField] Button SelectButton;

    public int Pete;
    public int Aj;
    public int Doozy;
    public int Mousey;
    public int Suzie;

    public List<int> CharsPrefs = new List<int>() {0, 0, 0, 0, 0};


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateCharPrefs();
    }
    public void ChangeCharacter(int num)
    {
        for(int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }

        number += num;

        if(number > Characters.Length - 1)
        {
            number = 0;
        }

        if(number < 0)
        {
            number = Characters.Length - 1;
        }

        if(number != selectedChar)
        {
            if (CharsPrefs[number] < 1)
            {
                SelectButtonText.text = "Locked";
                SelectButton.interactable = false;
            } else
            {
                SelectButton.interactable = true;
                SelectButtonText.text = "Select";
            }
        }
        else
        {
            SelectButton.interactable = true;
            SelectButtonText.text = "Selected";
            if (EventSystem.current && SelectButton)
            {
                EventSystem.current.SetSelectedGameObject(SelectButton.gameObject);
            }
        }
            Characters[number].SetActive(true);
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", number);
        selectedChar = number;
        SelectButtonText.text = "Selected";
        if (EventSystem.current && SelectButton)
        {
            EventSystem.current.SetSelectedGameObject(SelectButton.gameObject);
        }
    }
    public void SetUpSelector()
    {
        UpdateCharPrefs();
        if (PlayerPrefs.HasKey("SelectedCharacter"))
        {
            selectedChar = PlayerPrefs.GetInt("SelectedCharacter");
            number = selectedChar;
        }
        else
        {
            PlayerPrefs.SetInt("SelectedCharacter", 0);
            number = 0;
            selectedChar = 0;
        }

        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }

        SelectButtonText.text = "Selected";
        SelectButton.interactable = false;

        Characters[number].SetActive(true);

        if (EventSystem.current && SelectButton)
        {
            EventSystem.current.SetSelectedGameObject(SelectButton.gameObject);
        }
    }

    public void UpdateCharPrefs()
    {
        Pete = 1;
        Aj = PlayerPrefs.GetInt("AjChar");
        Doozy = PlayerPrefs.GetInt("DoozyChar");
        Mousey = PlayerPrefs.GetInt("MouseyChar");
        Suzie = PlayerPrefs.GetInt("SuzieChar");

        CharsPrefs[0] = Pete;
        CharsPrefs[1] = Aj;
        CharsPrefs[2] = Doozy;
        CharsPrefs[3] = Mousey;
        CharsPrefs[4] = Suzie;
    }
}
