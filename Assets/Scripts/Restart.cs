using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Save statistics of just finished game?
    private void OnCollisionEnter(Collision collision)
    {
        // restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
