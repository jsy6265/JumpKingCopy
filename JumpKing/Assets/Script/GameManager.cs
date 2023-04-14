using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace JumpKingCopy
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        private void Awake()
        {
            if (null == instance)
            {
                instance = this;

                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public bool isEnd;
        public int timmer;

        public EndGame endGame;

        public IEnumerator Timmer()
        {
            do
            {
                timmer++;

                yield return new WaitForSeconds(1f);

            } while (isEnd == false);

            yield return new WaitWhile(() => isEnd == false);

            endGame.End();
        }
    }

}
