using UnityEditor;
using UnityEngine;

namespace HQ
{
     class PlayerController: MonoBehaviour
    {
        public HqRenderer hQcamera;
        public ProjectedBody body;

        bool isPaused;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] bool useMyChanges;
        [SerializeField] int maxSpeed;
        [SerializeField] int offRoadDebuff;
        [SerializeField] int accerlationRate;

        private void Start()
        {
            isPaused = false;
        }

        private void FixedUpdate()
        {
           //Preserved orginal controls for the sake of demostration
            if (!useMyChanges)
            {
                body.speed = 0;
                if (Input.GetKey(KeyCode.RightArrow)) {   body.playerX += 0.1f; } 
                if (Input.GetKey(KeyCode.LeftArrow)) {  body.playerX -= 0.1f; }
                if (Input.GetKey(KeyCode.UpArrow)) body.speed = 200;
                if (Input.GetKey(KeyCode.DownArrow)) body.speed = -200;
                if (Input.GetKey(KeyCode.Tab)) body.speed *= 3;
                if (Input.GetKey(KeyCode.W)) hQcamera.cameraHeight += 100;
                if (Input.GetKey(KeyCode.S)) hQcamera.cameraHeight -= 100;
            }

            ///////////////////////////////////////////////////////////////////////
            //These are my major changes to the game
            ///////////////////////////////////////////////////////////////////////
            else
            {
                //Only move if not paused
                if (!isPaused) {
                   
                //Forward and Backward movemnt
                   //Move forward, acceralting graudally until max speed
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        if (body.speed < maxSpeed) {
                            body.speed += accerlationRate;
                           
                        }

                    }
                    //Move backward, going faster graudally until max speed
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        //If reverse speed is not maxed, then accerlate 
                        if (body.speed > (maxSpeed * -1))
                        {
                            body.speed -= accerlationRate;
                        }

                    }
                    //When not moving forward or backward, gradually slow to 0
                    if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) {
                        if (body.speed >= 50)
                        {
                            body.speed -= 50;
                        }
                        else if (body.speed <= -50) { body.speed += 50; }
                        else {
                            body.speed = 0;
                        }
                    }

                    //Left and Right controls
                    if (Input.GetKey(KeyCode.RightArrow)) body.playerX += 0.1f;
                    if (Input.GetKey(KeyCode.LeftArrow)) body.playerX -= 0.1f;
                    //If off road, apply speed penalty 
                    if (body.playerX > 1 || body.playerX < -1) {
                        if (body.speed >= 100)
                        {
                            body.speed -= offRoadDebuff;
                        }
                        else if (body.speed <= -100) { body.speed += offRoadDebuff; }
                        
                    }

                }

                //Triggers Pause menu
                if (Input.GetKeyDown(KeyCode.P)) PauseMenu();
            }
        }


        void PauseMenu() {
            //Game is already paused
            if (isPaused) { 
                isPaused=false;
                pauseMenu.SetActive(false);
                
            }

            //Pause game
            else {
                isPaused=true;
                pauseMenu.SetActive(true);
                body.speed = 0;
            }
        
        }
        
        }

    }

