using System;
using TMPro;
using UnityEngine;
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
        [SerializeField] TextMeshProUGUI speedText;

        public void FixedUpdate()
        {
            trip += speed;
            speedText.text = speed.ToString();

            while (trip >= track.Length * track.segmentLength) trip -= track.Length * track.segmentLength;
            while (trip < 0) trip += track.Length * track.segmentLength;
            playerPos = trip / track.segmentLength;
            playerX = playerX - track.lines[playerPos].curve * centrifugal * speed * Time.fixedDeltaTime;
            playerX = Mathf.Clamp(playerX, -2, 2);



        }

        public void SetSpeedZero() {
        
            speed = 0;
        }
    }
}
