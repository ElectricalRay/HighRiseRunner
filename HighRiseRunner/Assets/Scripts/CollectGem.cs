using UnityEngine;

public class CollectGem : CollectCollectible
{
    protected override void OnCollect()
    {
        MasterInfo.gemCount++;
    }
}
