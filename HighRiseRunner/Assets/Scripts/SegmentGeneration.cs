using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SegmentGeneration : MonoBehaviour
{
    public GameObject[] segments;
    public Transform player;

    [SerializeField] int spawnZ = 50;
    [SerializeField] int segmentLength = 50;
    [SerializeField] int segmentsAhead = 4;

    [SerializeField] bool creatingSegment = false;

    List<GameObject> generatedSegments = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < segmentsAhead; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if(player.position.z + (segmentsAhead * segmentLength) > spawnZ)
        {
            SpawnSegment();
            DeleteSegments();
        }
    }

    void SpawnSegment()
    {
        int segmentIndex = Random.Range(0, segments.Length);
        GameObject genSegment = Instantiate(segments[segmentIndex], new Vector3(0, 0, spawnZ), Quaternion.identity);
        generatedSegments.Add(genSegment);
        spawnZ += segmentLength;
    }

    void DeleteSegments()
    {
        while(generatedSegments.Count > 6)
        {
            Destroy(generatedSegments[0]);
            generatedSegments.RemoveAt(0);
        }
    }
}
