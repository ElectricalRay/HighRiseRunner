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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
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
            SelectButtonText.text = "Select";
        }
        else
        {
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

        Characters[number].SetActive(true);

        if (EventSystem.current && SelectButton)
        {
            EventSystem.current.SetSelectedGameObject(SelectButton.gameObject);
        }
    }
}
