using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(1);
    }
}
