using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpKingCopy
{
    public class PlayGame : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameManager.instance.isEnd = false;
            StartCoroutine(GameManager.instance.Timmer());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

