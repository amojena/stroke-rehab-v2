using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CashierSceneManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTextGUI;

    [SerializeField]
    GameObject fruitSpawnPoint;

    // Time in between two items spawning
    [SerializeField]
    float spawnDelay;

    [SerializeField]
    Floor floor;

    [SerializeField]
    Belt belt;

    [SerializeField]
    Crate[] crates;

    [SerializeField]
    GameObject[] items;



    float spawnTime;
    int itemsSpawned;
    string[] tags = { "Fruit", "Vegetable", "Dairy" };

    int itemsInCrate;


    // Start is called before the first frame update
    void Start()
    {
        itemsInCrate = 0;
        spawnTime = 0.0f;
        itemsSpawned = 0;
        ShuffleTags();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnItem();
        CountItemsInCrates();

        if (itemsInCrate + floor.itemsOnFloor() >= 12) GameOver();
        else UpdateScreenText(); // Format screen on text while game is going


        spawnTime += Time.deltaTime;
    }

    void UpdateScreenText()
    {
        scoreTextGUI.text = "Items spawned: " + itemsSpawned.ToString() + 
            "\n Items in crates: " + itemsInCrate;
    }

    void CountItemsInCrates()
    {
        int totalItems = 0;
        foreach (Crate crate in crates)
            totalItems += crate.getNumOfItemsInCrate();
        itemsInCrate = totalItems;
    }

    void SpawnItem()
    {
        // Too early to spawn new item
        if (spawnTime < spawnDelay || itemsSpawned >= 12) return;

        // Pick random item to spawn
        int randomIndex = Random.Range(0, 1000) % items.Length;
        GameObject itemToSpawn = items[randomIndex];
        Instantiate(itemToSpawn, fruitSpawnPoint.transform.position, itemToSpawn.transform.rotation);
        itemsSpawned++;
        spawnTime = 0.0f;
    }

    //Randomize what crates will hold what items (missing: tags displayed above crates)
    void ShuffleTags()
    {
        for(int i = 0; i < tags.Length; i++)
        {
            int j = Random.Range(0, tags.Length);
            string hold = tags[i];
            tags[i] = tags[j];
            tags[j] = hold;
        }

        for (int i = 0; i < 3; i++)
            crates[i].tag = tags[i];

    }

    void GameOver()
    {
        // Cognitive measurments (missing number of interactions)
        float score = 0;
        int incorrect = 0; // useful?
        float accuracy;
        int itemsOnFloor = floor.itemsOnFloor();

        foreach(Crate crate in crates)
        {
            score += crate.GetCorrectItemScore();
            incorrect += crate.GetIncorrectItemScore();
        }

        accuracy = (score / itemsSpawned) * 100f;

        // Format string with statistics
        scoreTextGUI.text = "Score: " + score.ToString();
        scoreTextGUI.text += "\nAccuracy: " + accuracy.ToString() + "%";
        scoreTextGUI.text += "\n Items dropped: " + itemsOnFloor.ToString();

        // Game visuals for game over (missing UI to restart)
        Time.timeScale = 0.0f;
        belt.gameObject.SetActive(false);
        foreach (Crate crate in crates)
        {
            crate.DestroyItems();
            crate.gameObject.SetActive(false);
        }
    }

}
