using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    Material correctMaterial;
    [SerializeField]
    Material incorrectMaterial;
    
    int correctItems;
    int incorrectItems;
    Material originalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        correctItems = 0;
        incorrectItems = 0;
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
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

        // Use Invoke to call function to revert to orig. material after 1s
        Invoke("RevertOriginalMaterial", 1f);
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

}
