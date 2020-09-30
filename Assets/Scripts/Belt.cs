using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField]
    GameObject fruitOrigin;

    [SerializeField]
    GameObject endOfBelt;

    [SerializeField]
    float spawnDelay;

    [SerializeField]
    float speedFactor;

    [SerializeField]
    GameObject[] items;


    float spawnTime;



    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime >= spawnDelay)
        {
            SpawnItem();
            spawnTime = 0.0f;
        }

        spawnTime += Time.deltaTime;
        
    }

    void SpawnItem()
    {
        int randomIndex = Random.Range(0, 1000) % items.Length;
        GameObject itemToSpawn = items[randomIndex];
        Instantiate(itemToSpawn, fruitOrigin.transform.position, itemToSpawn.transform.rotation);
    }

    void OnTriggerStay(Collider other)
    {
        other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, endOfBelt.transform.position, speedFactor * Time.deltaTime);
        Debug.Log(other.gameObject.name);
    }
}
