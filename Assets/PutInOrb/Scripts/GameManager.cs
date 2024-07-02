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
            // �ð��� �귯���� ���� ó��
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;
                if (gameTime < 0)
                {
                    gameTime = 0;
                }
                UpdateTimeText(); // ���� �ð��� ������Ʈ�մϴ�.
            }
            else
            {
                // Ÿ�̸Ӱ� 0�� �������� �� ó���� ������ ���⿡ �߰��� �� �ֽ��ϴ�.
            }
        }

        void UpdateTimeText()
        {
            // �ð��� ��:�� �������� ��ȯ�Ͽ� ǥ��
            int minutes = Mathf.FloorToInt(gameTime / 60F);
            int seconds = Mathf.FloorToInt(gameTime % 60F);
            timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

}