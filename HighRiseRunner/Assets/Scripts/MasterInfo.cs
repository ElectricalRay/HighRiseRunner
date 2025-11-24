using UnityEngine;


public class MasterInfo : MonoBehaviour
{
    public static int coinCount = 0;
    public static int gemCount = 0;
    public static int distanceRun = 0;
    [SerializeField] GameObject coinDisplay;
    [SerializeField] int internalDistance;
    [SerializeField] GameObject gemDisplay;
    [SerializeField] GameObject runDisplay;

    // Update is called once per frame
    void Update()
    {
        internalDistance = distanceRun;
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "" + coinCount;
        gemDisplay.GetComponent<TMPro.TMP_Text>().text = "" + gemCount;
        runDisplay.GetComponent<TMPro.TMP_Text>().text = "" + distanceRun;
    }
}
