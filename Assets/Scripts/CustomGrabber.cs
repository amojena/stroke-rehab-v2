using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabber : OVRGrabber
{
    [SerializeField]
    SceneController sceneController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    protected override void GrabBegin()
    {
        base.GrabBegin();
        if (m_grabbedObj) sceneController.AddInteraction();
    }
}
