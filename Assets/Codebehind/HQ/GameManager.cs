using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] string[] trackList;
    int currentTrack;
    string currentTrackName;
    [SerializeField] StringEvent onTrackUpdate;
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] UIManager UIManager;
    [SerializeField] List<Texture2D> trackPreviewList;
    public Dictionary<string, Texture2D> trackPreviewDic;
    [SerializeField] Texture2D noPreviewPicture;
    public int currentLap;

    // On awake, ensure there is only one instance of GameMangaer
    private void Awake()
    {
        //Set Current track/name
        currentTrack = 0;
        currentTrackName = trackList[currentTrack];

        //Set up dictionary of track preview pictures
        trackPreviewDic = new Dictionary<string, Texture2D>();
        for (int i = 0; i < trackList.Length; i++) {
            if (i < trackPreviewList.Count && trackPreviewList[i] != null)
            {
                // Assign the preview image to the corresponding track
                trackPreviewDic[trackList[i]] = trackPreviewList[i];
            }
            else {
                trackPreviewDic[trackList[i]] = noPreviewPicture;
            }

        }

    }

    //Loads Track Selection screen
    public void TrackSelect() {

        SceneManager.LoadScene("Selection Screen");
    
    }

    //Exits application
    public void Quit() { 
    
        Application.Quit();
    
    }

    //Loads a given track
    public void LoadTrack() {

        SceneManager.LoadScene(currentTrackName);
    }

    public void NextTrack() {

        
        //If not the last track in the list, return next
        if (currentTrack < trackList.Length - 1) {
            rightButton.SetActive(true);
            leftButton.SetActive(true);
            currentTrack += 1;
            currentTrackName = trackList[currentTrack];
            Debug.Log(currentTrackName);
            
            //Update UI
            onTrackUpdate.Invoke(currentTrackName);
        }
        
        //If the last track, disable next track button and ensure previous track button is there
        if (currentTrack == trackList.Length - 1)
        {
            rightButton.SetActive(false);
            leftButton.SetActive(true);
        }

    }

    public void PreviousTrack() {
        //If not the last track in the list, return next
        if (currentTrack > 0)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            currentTrack -= 1;
            currentTrackName = trackList[currentTrack];
            Debug.Log(currentTrackName);

            //Update UI
            onTrackUpdate.Invoke(currentTrackName);
        }

        //If first track, disable previous
        if (currentTrack == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
        }
    }

}


[Serializable]
public class StringEvent : UnityEvent<string> { }
[Serializable]
public class IntEvent : UnityEvent<int> { }
