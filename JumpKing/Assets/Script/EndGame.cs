using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace JumpKingCopy
{
    public class EndGame : MonoBehaviour
    {
        public GameObject endding;
        public TextMeshProUGUI clearTimeText;

        int hour;
        int min;
        int sec;

        private void Start()
        {
            GameManager.instance.endGame = this;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.isEnd = true;
            }
        }

        public void RestartGame()
        {
            GameManager.instance.timmer = 0;
            GameManager.instance.isEnd = false;
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void End()
        {
            endding.SetActive(true);

            min = GameManager.instance.timmer / 60;
            if (min > 60)
            {
                hour = GameManager.instance.timmer / 60;
            }
            else
            {
                hour = 0;
            }
            sec = GameManager.instance.timmer % 60;
            clearTimeText.SetText(hour + " : " + min + " : " + sec);
        }


    }
}

