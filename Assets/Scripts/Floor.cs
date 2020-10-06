using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    Material incorrectMaterial;

    [SerializeField]
    Crate[] crates;


    Material originalMaterial;
    int itemsDropped;



    // Start is called before the first frame update
    void Start()
    {
        itemsDropped = 0;
        originalMaterial = GetComponent<MeshRenderer>().material;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if item collided with a crate before falling to the floor, do nothing if so
        GameObject item = collision.gameObject;
        bool alreadySeen = crates[0].IsItemInCrate(item) || crates[1].IsItemInCrate(item) || crates[2].IsItemInCrate(item);
        if (alreadySeen) return;


        GetComponent<MeshRenderer>().material = incorrectMaterial;
        Invoke(nameof(RevertToOriginalMaterial), 1f);
        itemsDropped++;
    }

    void RevertToOriginalMaterial()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }

    public int ItemsOnFloor()
    {
        return itemsDropped;
    }
}
