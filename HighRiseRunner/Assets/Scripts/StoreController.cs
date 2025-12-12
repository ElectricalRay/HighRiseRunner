using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [SerializeField] GameObject gemsDisplay;
    [SerializeField] GameObject coinsDisplay;

    [SerializeField] GameObject AjButton;
    [SerializeField] GameObject DoozyButton;
    [SerializeField] GameObject MouseyButton;
    [SerializeField] GameObject SuzieButton;

    [SerializeField] GameObject AjBoughtButton;
    [SerializeField] GameObject DoozyBoughtButton;
    [SerializeField] GameObject MouseyBoughtButton;
    [SerializeField] GameObject SuzieBoughtButton;

    public int gems;
    public int coins;

    public int Aj;
    public int Doozy;
    public int Mousey;
    public int Suzie;

    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateOnEnter()
    {
        updateCurrencies();
        updateChars();
    }

    public void updateCurrencies()
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        gems = PlayerPrefs.GetInt("TotalGems");

        coinsDisplay.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + coins;
        gemsDisplay.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + gems;

        Aj = PlayerPrefs.GetInt("AjChar");
        Doozy = PlayerPrefs.GetInt("DoozyChar");
        Mousey = PlayerPrefs.GetInt("MouseyChar");
        Suzie = PlayerPrefs.GetInt("SuzieChar");
    }

    public void updateChars()
    {
        if(Aj > 0 && AjButton != null)
        {
            Destroy(AjButton);
            AjBoughtButton.SetActive(true);
        }
        if (Doozy > 0 && DoozyButton != null)
        {
            Destroy(DoozyButton);
            DoozyBoughtButton.SetActive(true);
        }
        if (Mousey > 0 && MouseyButton != null)
        {
            Destroy(MouseyButton);
            MouseyBoughtButton.SetActive(true);
        }
        if (Suzie > 0 && SuzieButton != null)
        {
            Destroy(SuzieButton);
            SuzieBoughtButton.SetActive(true);
        }
    }

    public void buyChar(int num)
    {
        gems = PlayerPrefs.GetInt("TotalGems");

        if (gems >= 10)
        {
            switch (num)
            {
                case 0:
                    PlayerPrefs.SetInt("AjChar", 1);
                    Aj = 1;
                    break;
                case 1:
                    PlayerPrefs.SetInt("DoozyChar", 1);
                    Doozy = 1;
                    break;
                case 2:
                    PlayerPrefs.SetInt("MouseyChar", 1);
                    Mousey = 1;
                    break;
                case 3:
                    PlayerPrefs.SetInt("SuzieChar", 1);
                    Suzie = 1;
                    break;
            }

            gems -= 10;

            PlayerPrefs.SetInt("TotalGems", gems);

            updateChars();
            updateCurrencies();
        }
    }
}
