using System;
using TMPro;
using UnityEngine;


namespace PutInOrb
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public bool isGameover = false;
        public float GetOrbduration = 0.5f;
        public float HitStunTime = 5f;
        public float Putingduration = 0.5f;


        public float gameTime = 120f;
        public int gamescore = 0;
        public int nomalOrbScore = 100;
        public int GoldOrbScore = 200;

        public bool IsFever;
        public int FeverScore;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timeText;
        public GameObject ChatchButton;
        public GameObject PutButton;
        public PlayerMovement player;

        public void SetPlayerState_Put()
        {
            ChatchButton.SetActive(false);
            PutButton.SetActive(true);
        }
        public void SetPlayerState_Chach()
        {
            Debug.Log("BBBCC");
            ChatchButton.SetActive(true);
            PutButton.SetActive(false);
        }

        public void GoldGoalIn()
        {
            gamescore += GoldOrbScore;

            scoreText.text = gamescore.ToString();
            if(IsFever && player.HitMeteoCount > 0)
            {
                IsFever = false;
            }
        }
        public void GoalIn()
        {

            gamescore += nomalOrbScore;


            scoreText.text = gamescore.ToString();
            if(!IsFever && gamescore >= FeverScore && player.HitMeteoCount == 0)
            {
                IsFever = true;
            }
            else if(IsFever && player.HitMeteoCount > 0)
            {
                IsFever = false;
            }
        }

        private void Awake()
        {
            if(instance == null) { instance = this; }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            // 시간이 흘러가는 것을 처리
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;
                if (gameTime < 0)
                {
                    gameTime = 0;
                }
                UpdateTimeText(); // 남은 시간을 업데이트합니다.
            }
            else
            {
                // 타이머가 0에 도달했을 때 처리할 로직을 여기에 추가할 수 있습니다.
            }
        }

        void UpdateTimeText()
        {
            // 시간을 분:초 형식으로 변환하여 표시
            int minutes = Mathf.FloorToInt(gameTime / 60F);
            int seconds = Mathf.FloorToInt(gameTime % 60F);
            timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

}