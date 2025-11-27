using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject coinsCount;
    [SerializeField] GameObject gemsCount;
    [SerializeField] GameObject distanceCount;
    public bool newRecord;
    private void OnEnable()
    {
        if(PlayerPrefs.HasKey("TotalCoins"))
        {
            int currentCoins = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.SetInt("TotalCoins", currentCoins + MasterInfo.coinCount);
        } else
        {
            PlayerPrefs.SetInt("TotalCoins", MasterInfo.coinCount);
        }

        if (PlayerPrefs.HasKey("TotalGems"))
        {
            int currentGems = PlayerPrefs.GetInt("TotalGems");
            PlayerPrefs.SetInt("TotalGems", currentGems + MasterInfo.gemCount);
        }
        else
        {
            PlayerPrefs.SetInt("TotalGems", MasterInfo.gemCount);
        }

        if(PlayerPrefs.HasKey("DesertHighScore"))
        {
            int currentHighScore = PlayerPrefs.GetInt("DesertHighScore");
            if(MasterInfo.distanceRun > currentHighScore)
            {
                newRecord = true;
                PlayerPrefs.SetInt("DesertHighScore", MasterInfo.distanceRun);
            } else
            {
                newRecord = false;
            }
        } else
        {
            PlayerPrefs.SetInt("DesertHighScore", MasterInfo.distanceRun);
            newRecord = true;
        }

        coinsCount.GetComponent<TMPro.TMP_Text>().text = "" + MasterInfo.coinCount;
        gemsCount.GetComponent<TMPro.TMP_Text>().text = "" + MasterInfo.gemCount;
        distanceCount.GetComponent<TMPro.TMP_Text>().text = "" + MasterInfo.distanceRun;

        PlayerPrefs.Save();
    }

    public void ContinueToMain()
    {
        StartCoroutine(MoveToMainMenu());
    }

    IEnumerator MoveToMainMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
