using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : Singleton<BoxSpawner>
{
    [Tooltip("List of box prefabs to spawn randomly from.")]
    List<GameObject> boxesPrefabs;
    List<GameObject>[] boxPools = new List<GameObject>[3];

    [SerializeField] GameObject box1Prefab;
    [SerializeField] GameObject box2Prefab;
    [SerializeField] GameObject box3Prefab;


    [SerializeField] Transform[] unloadingSpawnPos;
    [SerializeField] float spawnInterval = 5;
    //[SerializeField] int poolSize;
    
    [SerializeField] UnloadingTable unloadingTable;

    public bool automaticSpawning = false;

    [SerializeField] int totalBoxesLeft = 0;


    private void OnEnable()
    {
        //Create a pool of boxes according to the required percentage.

        for (int i = 0; i < boxPools.Length; i++)
        {
            boxPools[i] = new List<GameObject>();
        }


        CreateBoxPool(boxPools[0], GameManager.Instance.largeBoxAmount, box1Prefab);
        CreateBoxPool(boxPools[1], GameManager.Instance.mediumBoxAmount, box2Prefab);
        CreateBoxPool(boxPools[2], GameManager.Instance.squareBoxAmount, box3Prefab);

        totalBoxesLeft = boxPools[0].Count + boxPools[1].Count + boxPools[2].Count;

        SpawnBoxAtTable();
    }

    private void CreateBoxPool(List<GameObject> pool, int poolSize, GameObject prefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tempBox = Instantiate(prefab);
            if (tempBox != null)
            {
                tempBox.SetActive(false);
                pool.Add(tempBox);
            }
        }
    }


    public void AutoSpawnOn(bool on)
    {
        if(on)
        {
            StartCoroutine("SpawnAutomatically");
        } else
        {
            StopCoroutine("SpawnAutomatically");
        }

    }

    IEnumerator SpawnAutomatically()
    {
        GameObject box;
        box = GetRandomBox();
        while(box != null)
        {

            box.transform.position = unloadingSpawnPos[Random.Range(1,3)].position;
            box.transform.rotation = Quaternion.Euler(Vector3.zero);
            box.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
            box = GetRandomBox();
        }
    }
    //Spawn box at chosen location
    public void SpawnBoxAtTable()
    {
        if(!unloadingTable.isBoxOnTable)
        {
            GameObject spawnedBox = GetRandomBox();
            if (spawnedBox != null)
            {
                spawnedBox.transform.position = unloadingSpawnPos[0].position;
                spawnedBox.transform.rotation = Quaternion.Euler(Vector3.zero);
                spawnedBox.SetActive(true);
            }
        }
    }

    GameObject GetRandomBox()
    {
        while (totalBoxesLeft != 0)
        {
            var poolIndex = Random.Range(0, 3);
            var tempBox = GetBoxFromPool(boxPools[poolIndex]);
            if (tempBox != null)
            {
                boxPools[poolIndex].Remove(tempBox);
                totalBoxesLeft--;
                UIManager.Instance.UpdateBoxesAmount(boxPools[0].Count, boxPools[1].Count, boxPools[2].Count);
                return tempBox;
            }
        }
        return null;
    }

    GameObject GetBoxFromPool(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }
        return null;
    }
}
