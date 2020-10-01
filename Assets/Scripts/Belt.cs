using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    List<GameObject> itemsToMove = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer to spawn items
        if (spawnTime >= spawnDelay)
        {
            SpawnItem();
            spawnTime = 0.0f;
        }

        spawnTime += Time.deltaTime;

        MoveItemsOnBelt();
        
    }

    void MoveItemsOnBelt()
    {
        foreach(GameObject item in itemsToMove)
        {
            item.transform.position = Vector3.Lerp(item.transform.position, endOfBelt.transform.position, speedFactor * Time.deltaTime);
        }
    }

    void SpawnItem()
    {
        // Pick random item to spawn
        int randomIndex = Random.Range(0, 1000) % items.Length;
        GameObject itemToSpawn = items[randomIndex];
        Instantiate(itemToSpawn, fruitOrigin.transform.position, itemToSpawn.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        itemsToMove.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        itemsToMove.Remove(collision.gameObject);
    }

}
