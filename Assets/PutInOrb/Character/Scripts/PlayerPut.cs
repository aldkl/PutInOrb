using PutInOrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PutInOrb
{
    public class PlayerPut : MonoBehaviour
    {

        public PlayerInput playerInput;
        public PlayerMovement playermovement;
        public bool IsPutOrb;
        public Collider2D Oncollision;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Hole"))
            {
                Oncollision = collision;
                IsPutOrb = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Hole"))
            {
                Oncollision = null;
                IsPutOrb = false;
            }
        }
        public void Update()
        {
            if (IsPutOrb && playerInput.put)
            {
                if (Oncollision != null)
                {
                    IsPutOrb = false;
                    playermovement.movingType = State.Putting;
                    playermovement.StartCoroutine("ChangeStatePut");

                    if(Oncollision.GetComponent<Hole>().type == playermovement.transform.GetChild(3).GetComponent<Orb>().type)
                    {
                        Debug.Log("aa");
                        GameManager.instance.GoalIn();
                    }
                    else if(playermovement.transform.GetChild(3).GetComponent<Orb>().type == -1)
                    {
                        GameManager.instance.GoldGoalIn();
                    }
                    Destroy(playermovement.transform.GetChild(3).gameObject);
                }
            }
        }
    }
}