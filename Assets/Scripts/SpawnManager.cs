using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject spawnLocs;
    [SerializeField] ItemList herbList;
    private List<float> probBounds = new();
    private int herbCount;
    private int iSpawnLocs;
    private int herbIndex = 0;
    private float lowerBound = 0;
    private float range = 0;
    private List<float> midPoints = new();
    private List<float> radius = new();
    private float radiusChange;
    // Start is called before the first frame update
    void Start()
    {
        herbCount = herbList.itemList.Count;
        iSpawnLocs = spawnLocs.transform.childCount;
        ProbInit();
        radiusChange = 1.0f/(2.0f*(float)herbCount*(float)iSpawnLocs);

        foreach (Transform spawnLoc in spawnLocs.transform)
        {
            herbIndex = GenNum();
            //start the spawn chance at equal. As the locations decrease and herbs spawn the herbs with the least spawns get a higher chance to spawn.
            //probability. accessed by the random num generator to decide which herb to spawn. also accessed by prob balancer to balance the probabilities.
            GameObject spawnedHerb = Instantiate(herbList.itemList[herbIndex].itemPrefab, spawnLoc);
            spawnedHerb.GetComponent<Rigidbody>().isKinematic = true;
            //ProbBalancer
            ProbBalancer(herbIndex);
        }
    }

    private void ProbInit()
    {
        //should initialise the boundaries which decide which herb to spawn at equal probability for each herb where the last number should be 1.
        float initSpacing = 1.0f/(float)herbCount;
        float cumulativeSpacing = 0;
        for(int i = 0; i < herbCount; i ++)
        {
            cumulativeSpacing += initSpacing;
            probBounds.Add(cumulativeSpacing);
        }

        foreach (float upperBounds in probBounds)
        {
            //generates the first set of radius and midpoints that are needed to caluclate the new probabilities as the herbs get spawned.
            range = upperBounds - lowerBound;
            float _radius = range/2.0f;
            radius.Add(_radius);
            midPoints.Add(_radius+lowerBound);

            lowerBound = upperBounds;
        }
    }
    private int GenNum()
    {
        //gen random num, depending on the range means that x herb gets spawned.
        float rnd = Random.value;
        for (int i = 0; i < herbCount; i++)
        {
            if (rnd < probBounds[i])
            {
                return i;
            }
        }
        return 0;
    }
    void ProbBalancer(int _herbIndex)
    {
        //takes the range and balances it after taking in the latest item in.
        //calculate mid point between all boundaries.
        //decrease the "influence" of the chosen herb by ()


        for (int i = 0; i < herbCount; i++)
        {
            //if the iteration reaches the spawned herb then reduce the prob else increase it.
            if (i == _herbIndex) {radius[i] -= radiusChange;}
            
            else{radius[i] += radiusChange/(float)(herbCount-1);}
        }

        float shiftAmount = 0 - (midPoints[0] - radius[0]);
        for(int i = 0; i < herbCount; i++)
        {
            midPoints[i] += shiftAmount;
            probBounds[i] = midPoints[i] + radius[i];
        }
    }
}
