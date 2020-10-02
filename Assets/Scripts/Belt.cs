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
    float speedFactor;

    List<GameObject> itemsToMove = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveItemsOnBelt();
    }

    void MoveItemsOnBelt()
    {
        foreach(GameObject item in itemsToMove)
            item.transform.position = Vector3.Lerp(item.transform.position, endOfBelt.transform.position, speedFactor * Time.deltaTime);
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
