using UnityEngine;

public class MyVideoPlayer : MonoBehaviour
{
    public UnityEngine.Video.VideoClip videoClip;
    private Touch theTouch;
    public GameObject overlayGO;

    void Start()
    {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        var audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        // Restart from beginning when done.
        videoPlayer.isLooping = true;
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }
     

    void Update()
    {
        if(Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if(theTouch.phase == TouchPhase.Ended)
            {
                //touch detected! 
                //check if the touch was at the ImageTarget?
                Ray raycast = Camera.main.ScreenPointToRay(theTouch.position); //shoot ray from cam, to the object kena touched
                RaycastHit raycastHit; //contains the "context of the kena hit"
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == gameObject.name)
                    {
                        Debug.Log("User touch hit GO: " + gameObject.name);
                        toggleVideoPlaying();
                    }
                     
                }
                 
            }
        } 
    }

    private void toggleVideoPlaying()
    {
        var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
        if (vp.isPlaying)
        {
            Debug.Log("Video pause!");
            vp.Pause();
            overlayGO.GetComponent<PlayPauseOverlay>().ShowPlayTexture();
        }
        else
        {
            Debug.Log("Video play!");
            vp.Play();
            overlayGO.GetComponent<PlayPauseOverlay>().ShowNoTexture();
        }
    }

     
}