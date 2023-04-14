using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpKingCopy
{
    public class Player : MonoBehaviour
    {
        public int playerSpeed = 5;

        public float jumpSpeed;
        public float maxJump;

        public bool isCharge;
        public bool isfolling;

        Rigidbody2D rigid;
        Animator animator;

        public ParticleSystem chargeParticle;
        public Transform transf;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            transf = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            CheakJump();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isEnd == false)
            {
                PlayerMove();
                PlayerJump();
            }
        }


        void PlayerMove()
        {
            if (Input.GetAxisRaw("Horizontal") > 0 && isCharge == false && isfolling == false)
            {
                transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
            }
            if (Input.GetAxisRaw("Horizontal") < 0 && isCharge == false && isfolling == false)
            {
                transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKey(KeyCode.Space) && isfolling == false)
            {
                isCharge = true;
                StartCoroutine(Jumpcharging());
            }

        }

        void PlayerJump()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                chargeParticle.Stop();
                isCharge = false;
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    rigid.AddForce(new Vector2(playerSpeed, jumpSpeed), ForceMode2D.Impulse);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    rigid.AddForce(new Vector2(-playerSpeed, jumpSpeed), ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }

                jumpSpeed = 5;
            }
        }
        IEnumerator CheakPos(float nowpos)
        {

            yield return new WaitForSeconds(0.001f);

            float oldpos = GetComponent<Transform>().position.y;

            if (nowpos != oldpos)
            {
                isfolling = true;
            }
            else
            {
                isfolling = false;
            }



        }

        void CheakJump()
        {
            float nowpos = transf.position.y;

            StartCoroutine(CheakPos(nowpos));

        }


        IEnumerator Jumpcharging()
        {
            ParticleSystem.MainModule mainModule = chargeParticle.main;
            Color mid = new Color(1f, 0.2f, 0f);
            Color max = new Color(1f, 0.1f, 0f);
            if (Input.GetKeyDown(KeyCode.Space) != false)
            {
                chargeParticle.Play();
                do
                {
                    if (jumpSpeed > 9 && jumpSpeed < 15)
                    {
                        mainModule.startColor = mid;
                    }
                    else if (jumpSpeed == 15)
                    {
                        mainModule.startColor = max;
                    }
                    if (jumpSpeed < maxJump)
                    {
                        jumpSpeed++;
                    }
                    yield return new WaitForSeconds(0.25f);
                } while (isCharge == true);
            }
            yield return new WaitWhile(() => Input.GetKeyUp(KeyCode.Space) == true);
        }


    }

}
