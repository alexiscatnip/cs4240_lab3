using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPauseOverlay : MonoBehaviour
{
    public Texture2D playTexture;
    public Texture2D pauseTexture;
    Renderer m_Renderer;


    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        //ShowPlayTexture();
    }
    public void ShowPlayTexture ()
    {
        m_Renderer.enabled = true; 
        m_Renderer.material.SetTexture("_MainTex", playTexture);
    }
    public void ShowNoTexture()
    {
        m_Renderer.enabled = false; //stop rendering
    }
}
