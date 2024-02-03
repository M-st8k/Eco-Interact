using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component from the GameObject this script is attached to
        videoPlayer = GetComponent<VideoPlayer>();

        // Set the video clip (replace "Assets/YourVideoFile.mp4" with the actual path)
        videoPlayer.url = "Assets/Others/ECOLOGICAL-LEVEL-OF-ORGANIZATION-IN-AN-ECOSYSTEM.mp4";

        // Subscribe to the videoPlayer's prepareCompleted event
        videoPlayer.prepareCompleted += VideoPlayer_PrepareCompleted;

        // Prepare the video player
        videoPlayer.Prepare();
    }

    // Callback when video preparation is completed
    private void VideoPlayer_PrepareCompleted(VideoPlayer vp)
    {
        // Start playing the video
        vp.Play();
    }

    // Pause the video
    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

    // Fast forward the video by a specified number of seconds
    public void FastForward(float seconds)
    {
        videoPlayer.time += seconds;
    }
}