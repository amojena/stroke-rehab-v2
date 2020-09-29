using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    [SerializeField]
    GameObject fruitOrigin;

    [SerializeField]
    GameObject conveyorBelt;

    [SerializeField]
    GameObject[] items;

    Transform fruitStart;
    bool gen = false;


    // Start is called before the first frame update
    void Start()
    {
        fruitStart = fruitOrigin.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gen)
        {
            foreach (GameObject item in items)
            {
                Instantiate(item, fruitStart.position, item.transform.rotation);
            }

            gen = true;

        }

    }
}
