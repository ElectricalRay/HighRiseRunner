using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] GameObject gems;
    [SerializeField] GameObject coins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCurrencies()
    {
        coins.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + PlayerPrefs.GetInt("TotalCoins");
        gems.gameObject.GetComponent<TMPro.TMP_Text>().text = "" + PlayerPrefs.GetInt("TotalGems");
    }
}
