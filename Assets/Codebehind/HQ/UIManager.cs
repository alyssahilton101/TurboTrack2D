using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI trackNameText;
    [SerializeField] GameObject currentTrackPreview;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI lapText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameManager gameManager;

    int currentLap;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLap = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Updates the currently shown track
    public void UpdateCurrentTrack(string trackName) { 
    
        trackNameText.text = trackName;
        currentTrackPreview.GetComponent<RawImage>().texture = gameManager.trackPreviewDic[trackName]; 
    }

   //Update speed UI
    public void UpdateSpeed(int speed) { 
        string speedString = speed.ToString();
        speedText.text = speedString + " m/h"; 
    }

    //Update lap UI
    public void UpdateLap() {
        currentLap += 1;
        lapText.text = "Lap: " + currentLap.ToString();
    }

    //Activate gameover UI
    public void GameOverUI() { 
        gameOverUI.SetActive(true);
    }


}
