using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    TextMeshPro scoreTMP;

    [SerializeField]
    GameObject fruitSpawnPoint;

    // Time in between two items spawning
    [SerializeField]
    float spawnDelay;

    [SerializeField]
    Floor floor;

    [SerializeField]
    Belt belt;

    // Cube that will on collision will allow to replay the game
    [SerializeField]
    GameObject restartSphere;

    [SerializeField]
    Crate[] crates;

    // Items that are available to spawn
    [SerializeField]
    GameObject[] items;

    [SerializeField]
    GameObject leftHandAnchor, rightHandAnchor;

    //Private variables

    int itemsInCrate = 0;
    int itemsSpawned = 0;

    float spawnTime = 0f; // stopwatch used to determine if it is time to spawn a new item

    string[] tags = { "Fruit", "Vegetable", "Dairy" };

    float totalHandDistance;
    float totalTime;

    Vector3 leftHandPrevPos, rightHandPrevPos;
    Vector3 leftStartPos, rightStartPos;
    float maxDistance;

    int interactions = 0;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleTags();
        totalHandDistance = 0f;
        totalTime = 0f;
        maxDistance = 0f;
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;

        SpawnItem();
        CountItemsInCrates();

        if (itemsInCrate + floor.ItemsOnFloor() >= 12) GameOver();
        else UpdateScreenText(); // Format screen on text while game is going

        UpdateHandDistance();

        spawnTime += Time.deltaTime;
        totalTime += Time.deltaTime;
    }

    void UpdateHandDistance()
    {
        if (leftHandPrevPos == null && rightHandPrevPos == null)
        {
            leftHandPrevPos = leftHandAnchor.transform.position;
            leftStartPos = leftHandPrevPos;

            rightHandPrevPos = rightHandAnchor.transform.position;
            rightStartPos = rightHandPrevPos;
            return;
        }

        // Calculate distance between current pos and previous pos
        float leftTempDistance = Vector3.Distance(leftHandAnchor.transform.position, leftHandPrevPos);
        float rightTempDistance = Vector3.Distance(rightHandAnchor.transform.position, rightHandPrevPos);
        float totalTempDistance = leftTempDistance + rightTempDistance;
        totalHandDistance += totalTempDistance;

        // Calculate distance from current pos and starting pos (for max dist reached)
        leftTempDistance = Vector3.Distance(leftHandAnchor.transform.position, leftStartPos);
        rightTempDistance = Vector3.Distance(rightHandAnchor.transform.position, rightStartPos);
        totalTempDistance = leftTempDistance + rightTempDistance;
        if (totalTempDistance > maxDistance) maxDistance = totalTempDistance;

        leftHandPrevPos = leftHandAnchor.transform.position;
        rightHandPrevPos = rightHandAnchor.transform.position;
    }

    void UpdateScreenText()
    {
        scoreTMP.text = "Items spawned: " + itemsSpawned.ToString() + 
            "\n Items in crates or floor: " + itemsInCrate;
    }

    void CountItemsInCrates()
    {
        int totalItems = 0;
        foreach (Crate crate in crates) totalItems += crate.GetNumOfItemsInCrate();
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
        {
            crates[i].UpdateTagText(tags[i]);
        }

    }

    void GameOver()
    {
        gameOver = true;

        // Cognitive measurments (missing number of interactions)
        float score = 0;
        int incorrect = 0; // useful?
        float accuracy;
        int itemsOnFloor = floor.ItemsOnFloor();

        foreach(Crate crate in crates)
        {
            score += crate.GetCorrectItemScore();
            incorrect += crate.GetIncorrectItemScore();
        }

        accuracy = (score / itemsSpawned) * 100f;

        float avgHandSpeed = totalHandDistance / totalTime;

        // Format string with statistics
        scoreTMP.text = "Score: " + score.ToString();
        scoreTMP.text += "\nAccuracy: " + accuracy.ToString() + "%";
        scoreTMP.text += "\n Items dropped: " + itemsOnFloor.ToString();
        scoreTMP.text += "\n interactions: " + interactions.ToString();
        scoreTMP.text += "\n Avg Hand Speed: " + avgHandSpeed.ToString();
        scoreTMP.text += "\n Total Distance: " + totalHandDistance.ToString();
        scoreTMP.text += "\n Max Distance Reached: " + maxDistance.ToString();
        scoreTMP.fontSize = 6f;


        // Game visuals for game over (missing UI to restart)
        Time.timeScale = 0.0f;
        belt.gameObject.SetActive(false);
        foreach (Crate crate in crates)
        {
            crate.DestroyItems();
            crate.gameObject.SetActive(false);
        }

        // place restart sphere where the middle crate was
        restartSphere.transform.position = crates[1].transform.position + new Vector3(0, .1f, .1f);
    }

    // Called from Custom Grabber
    public void AddInteraction()
    {
        interactions++;
    }



}
