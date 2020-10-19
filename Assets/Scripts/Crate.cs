using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crate : MonoBehaviour
{
    [SerializeField]
    Material correctMaterial;
    [SerializeField]
    Material incorrectMaterial;
    [SerializeField]
    Crate[] otherCrates;
    
    int correctItems;
    int incorrectItems;
    Material originalMaterial;
    List<GameObject> itemsInCrate;

    // Start is called before the first frame update
    void Start()
    {
        correctItems = 0;
        incorrectItems = 0;
        itemsInCrate = new List<GameObject>();
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        GetComponentInChildren<TextMeshPro>().text = tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Don't do anything if the item has alredy collided with this crate or others
        if (ItemInOtherCrates(collision.gameObject)) return;


        if (gameObject.CompareTag(collision.gameObject.tag))
        {
            correctItems++;
            gameObject.GetComponent<MeshRenderer>().material = correctMaterial;
        }

        else
        {
            incorrectItems++;
            gameObject.GetComponent<MeshRenderer>().material = incorrectMaterial;
        }


        itemsInCrate.Add(collision.gameObject);
        Invoke(nameof(RevertOriginalMaterial), 1f);
    }

    void RevertOriginalMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
    }

    public int GetCorrectItemScore()
    {
        return correctItems;
    }

    public int GetIncorrectItemScore()
    {
        return incorrectItems;
    }

    public int GetNumOfItemsInCrate()
    {
        return itemsInCrate.Count;
    }

    public void DestroyItems()
    {
        foreach (GameObject item in itemsInCrate) item.SetActive(false);
    }

    public bool IsItemInCrate(GameObject item)
    {
        return itemsInCrate.Contains(item);
    }
    public bool ItemInOtherCrates(GameObject item)
    {
        bool ret = IsItemInCrate(item);
        foreach (Crate crate in otherCrates)  ret = ret || crate.IsItemInCrate(item);
        return ret;
    }

    public void UpdateTagText(string newTag)
    {
        tag = newTag;
        GetComponentInChildren<TextMeshPro>().text = tag;
    }

}
