using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityInterface : MonoBehaviour
{
    //an interface, accessible by outsiders, that can toggle this GO's isEnable
    public void Disable()
    {
        this.enabled = false;
        this.gameObject.GetComponent<MyVideoPlayer>().pauseVideo();
    }

    public void Enable()
    {
        this.enabled = true;

    }

}
