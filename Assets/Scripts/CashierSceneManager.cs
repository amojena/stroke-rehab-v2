using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CashierSceneManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTextGUI;

    [SerializeField]
    GameObject fruitSpawnPoint;

    // Time in between two items spawn
    [SerializeField]
    float spawnDelay;

    [SerializeField]
    Crate[] crates;

    [SerializeField]
    GameObject[] items;


    float spawnTime;
    int itemsSpawned;
    string[] tags = { "Fruit", "Vegetable", "Dairy" };

    int score;
    int incorrect;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        incorrect = 0;
        spawnTime = 0.0f;
        itemsSpawned = 0;
        ShuffleTags();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnItem();
        spawnTime += Time.deltaTime;

        if (itemsSpawned == 12)
            GameOver();
        else
            scoreTextGUI.text = "Items spawned: " + itemsSpawned.ToString();
    }

    void SpawnItem()
    {
        // Too early to spawn new item
        if (spawnTime < spawnDelay) return;

        // Pick random item to spawn
        int randomIndex = Random.Range(0, 1000) % items.Length;
        GameObject itemToSpawn = items[randomIndex];
        Instantiate(itemToSpawn, fruitSpawnPoint.transform.position, itemToSpawn.transform.rotation);
        itemsSpawned++;
        spawnTime = 0.0f;
    }

    void ShuffleTags()
    {
        for(int i = 0; i < tags.Length; i++)
        {
            int j = Random.Range(0, tags.Length);
            string hold = tags[i];
            tags[i] = tags[j];
            tags[j] = hold;
        }
    }

    void GameOver()
    {
        foreach(Crate crate in crates)
        {
            score += crate.GetCorrectItemScore();
            incorrect += crate.GetIncorrectItemScore();
        }

        scoreTextGUI.text = "Score: " + score.ToString();

        Time.timeScale = 0.0f;
    }
}
