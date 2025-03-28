using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace HQ
{
    public class ProjectedBody : MonoBehaviour
    {
        internal float playerX;
        internal int speed;
        public TrackObject track;
        [NonSerialized]
        private int playerPos;
        public float centrifugal = 0.1f;
        public int trip;
       
        ///My changes/////////////////////////////
        int currentLap;
        [SerializeField] int maxLaps;
        [SerializeField] UnityEvent onFinsh;
        [SerializeField] IntEvent onSpeedUpdate;
        [SerializeField] UnityEvent onLapUpdate;
        [SerializeField] PlayerController playerController;
        bool isDone;
        bool hitCheckpoint1;
        bool hitCheckpoint2;
        int checkPoint1;
        int checkPoint2;
        ///////////////////////////////////////////
       
        //Added start method to intialize my new values
        private void Start()
        {
            isDone = false;
            hitCheckpoint1 = false;
            hitCheckpoint2 = false;
            currentLap = 0;
            checkPoint1 = track.Length / 3;
           // checkPoint1 = 300;
            checkPoint2 = 1000;
        }

        public void FixedUpdate()
        {
            //Added if statement to check if the race is complete 
            if (!isDone) {
               //Add current speed to distance traveled (not mine)
                trip += speed;
                onSpeedUpdate.Invoke(speed);

                //Check if we hit a checkpoint or if we finished a lap (mine)
                CheckLapComplete();
                CheckCheckPoint();

                //Code for updating where player is (not mine)
                while (trip >= track.Length * track.segmentLength) trip -= track.Length * track.segmentLength;
                while (trip < 0) trip += track.Length * track.segmentLength;
                playerPos = trip / track.segmentLength;
                playerX = playerX - track.lines[playerPos].curve * centrifugal * speed * Time.fixedDeltaTime;
                playerX = Mathf.Clamp(playerX, -2, 2);
            }

        }

        //Sets speed to zero
        public void SetSpeedZero() {
        
            speed = 0;
        }

        //Increments current lap by 1 if all checkpoints were hit
        //This ensures you cannot go backwards to get another lap
        void CheckLapComplete() {


            if (trip < 0 || trip > track.Length * track.segmentLength)
            {
                //Check to see if player hit checkpoints
                if (hitCheckpoint1 && hitCheckpoint2) {
                    currentLap += 1; 
                    hitCheckpoint1 = false;
                    hitCheckpoint2 = false;

                    //Check to see if player finished
                    if (currentLap != maxLaps)
                    {
                        onLapUpdate.Invoke();
                    }
                    else
                    {
                        onFinsh.Invoke();
                        isDone = true;
                    }
                }

            }
            
        }

        //Check to see if player has hit checkpoints
        void CheckCheckPoint() {

            //If we didn't hit the checkpoint before with our trip but have now, set to true
            if (trip - speed < checkPoint1 && trip >= checkPoint1) { 
                hitCheckpoint1 = true;
                Debug.Log("Hit Checkpoint 1");
            }
            if (trip - speed < checkPoint2 && trip >= checkPoint2)
            {
                hitCheckpoint2 = true;
                Debug.Log("Hit Checkpoint 2");
            }
            if (trip - speed >= checkPoint1 && trip < checkPoint1)
            {
                hitCheckpoint1 = false;
                Debug.Log("Unhit Checkpoint 1");
            }
            if (trip - speed >= checkPoint2 && trip < checkPoint2)
            {
                hitCheckpoint2 = false;
                Debug.Log("Unhit Checkpoint 2");
            }

        }

       
    }
}
