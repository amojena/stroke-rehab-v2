using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    Material incorrectMaterial;


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
        GetComponent<MeshRenderer>().material = incorrectMaterial;
        Invoke(nameof(RevertToOriginalMaterial), 1f);
        itemsDropped++;
    }

    void RevertToOriginalMaterial()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }

    public int itemsOnFloor()
    {
        return itemsDropped;
    }
}
