using UnityEngine;

public class CollectCoin : CollectCollectible
{
    protected override void OnCollect()
    {
        MasterInfo.coinCount++;
    }
}
