using UnityEditor;
using UnityEngine;

namespace HQ
{
     class PlayerController: MonoBehaviour
    {
        public HqRenderer hQcamera;
        public ProjectedBody body;

        public bool isPaused;
        bool isMovementPaused;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] bool useMyChanges;
        [SerializeField] int maxSpeed;
        [SerializeField] int offRoadDebuff;
        [SerializeField] int accerlationRate;
        [SerializeField] float handling;
        [SerializeField] bool isSlippery;
        [SerializeField] float slipRate;

        bool waslastGoingRight;
        int pastSpeed;

        private void Start()
        {
            isPaused = false;
            waslastGoingRight = false;
            pastSpeed = 0;
            isMovementPaused = false;
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
                if (!isPaused && !isMovementPaused) {
                   
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
                    if (Input.GetKey(KeyCode.RightArrow)) {
                        body.playerX += handling;
                        waslastGoingRight = true;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow)) {
                        body.playerX -= handling;
                        waslastGoingRight = false;
                    }
                    //If off road, apply speed penalty 
                    if (body.playerX > 1 || body.playerX < -1) {
                        if (body.speed >= 100)
                        {
                            body.speed -= offRoadDebuff;
                        }
                        else if (body.speed <= -100) { body.speed += offRoadDebuff; }
                        
                    }
                    //If slippery
                    if (isSlippery && (!Input.GetKey(KeyCode.RightArrow) || !Input.GetKey(KeyCode.LeftArrow))) {

                        if (waslastGoingRight) body.playerX += slipRate;
                        else { body.playerX -= slipRate; }
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
                isMovementPaused=false;
                pauseMenu.SetActive(false);
                body.speed = pastSpeed;
                
            }

            //Pause game
            else {
                isPaused=true;
                pauseMenu.SetActive(true);
                PauseMovement();
            }
        
        }

        public void PauseMovement() {
            pastSpeed = body.speed;
            body.speed = 0;
            isMovementPaused = true;
        }

        public void SetSlipperyOn()
        {
            isSlippery = true;
            if (body.speed >= 100)
            {
                body.speed -= offRoadDebuff * (body.speed / 100);
            }
            else if (body.speed <= -100) { body.speed += offRoadDebuff * 4; }
        }

        public void SetSlipperyOff() { 
            isSlippery=false;
        }

    }

    }

