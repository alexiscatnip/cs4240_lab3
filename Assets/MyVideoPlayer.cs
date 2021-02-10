using UnityEngine;

public class MyVideoPlayer : MonoBehaviour
{
    //handles the:
    //1 touch detection of the finger touch the picture.
    //2 play the video
    //3 activate and deactivate the play and pause overlay
    //4 set time of the UI time indicator
    

    public UnityEngine.Video.VideoClip videoClip;
    private Touch theTouch;
    public GameObject overlayGO;
    private UnityEngine.Video.VideoPlayer vp;
    public GameObject timeUItext;
    private int vidDuration;

    void Start(){ 
        InitVideoPlayer();

    }

    void InitVideoPlayer() //Known Bug: video will start from the beginning if this gameobject is DISABLED (this occurs when the image goes out of tracking(frame)). this is because the video init is put inside start(). see unity GO lifecycle.
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

        vp = videoPlayer;
        vidDuration = (int)vp.clip.length;
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
                    if (raycastHit.collider == gameObject.GetComponent<Collider>())
                    {
                        Debug.Log("User touch hit GO: " + gameObject.name);
                        toggleVideoPlaying();
                    }
                     
                }
                 
            }
        }
        //update ui time display
        timeUItext.GetComponent<TMPro.TMP_Text> ().text =  ((int)vp.time).ToString() + " / " +vidDuration.ToString();
    }

    private void toggleVideoPlaying()
    {
        if (vp.isPlaying)
        {
            pauseVideo();
        }
        else
        {
            playVideo();
        }
    }

    public void playVideo()
    {
        Debug.Log("Video play!");
        vp.Play();
        overlayGO.GetComponent<PlayPauseOverlay>().ShowNoTexture();
    }

    public void pauseVideo()
    {
        Debug.Log("Video pause!");
        vp.Pause();
        overlayGO.GetComponent<PlayPauseOverlay>().ShowPlayTexture();
    }

}