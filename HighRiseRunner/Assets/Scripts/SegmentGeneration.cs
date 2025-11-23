using System.Collections;
using UnityEngine;

public class SegmentGeneration : MonoBehaviour
{
    public GameObject[] segments;

    [SerializeField] int zPos = 50;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if(!creatingSegment)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }
    }

    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, segments.Length);
        Instantiate(segments[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;

        yield return new WaitForSeconds(2);
        creatingSegment = false;
    }
}
