using UnityEngine;

public class CharactorSelector : MonoBehaviour
{
    public GameObject[] Characters;
    public int number;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if(PlayerPrefs.HasKey("SelectedCharacter"))
        {
            number = PlayerPrefs.GetInt("SelectedCharacter");
        } else
        {
            PlayerPrefs.SetInt("SelectedCharacter", 0);
            number = 0;
        }
        Characters[number].SetActive(true);
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

        Characters[number].SetActive(true);
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", number);
    }
}
