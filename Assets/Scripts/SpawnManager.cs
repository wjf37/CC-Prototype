using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject spawnLocs;
    [SerializeField] ItemList herbList;
    // Start is called before the first frame update
    void Start()
    {
        ProbInit(herbList.itemList.Count);
        foreach (Transform spawnLoc in spawnLocs.transform)
        {
            //start the spawn chance at equal. As the locations decrease and herbs spawn the herbs with the least spawns get a higher chance to spawn.
            //probability. accessed by the random num generator to decide which herb to spawn. also accessed by prob balancer to balance the probabilities.
            GameObject spawnedHerb = Instantiate(herbList.itemList[Random.Range(0,herbList.itemList.Count-1)].itemPrefab, spawnLoc);
            spawnedHerb.GetComponent<Rigidbody>().isKinematic = true;
            //ProbBalancer
        }
    }

    private void ProbInit(int herbCount)
    {
        float initSpacing = 1/herbCount;
        float rnd = Random.value;
    }
    private int GenNum(int spawnLocs)
    {
        //gen random num, depending on the range means that x herb gets spawned.

        return 0;
    }
    void ProbBalancer(ItemData item)
    {
        //takes the range and balances it after taking in the latest item in.
    }
}
