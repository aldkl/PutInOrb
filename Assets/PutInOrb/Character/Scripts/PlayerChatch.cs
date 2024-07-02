using PutInOrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PutInOrb
{
    public class PlayerChatch : MonoBehaviour
    {

        public PlayerInput playerInput;
        public PlayerMovement playermovement;
        public Vector2 targetVec2;
        public bool IsOnOrb;
        public Collider2D Oncollision;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Orb"))
            {
                Oncollision = collision;
                IsOnOrb = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Orb"))
            {
                Oncollision = null;
                IsOnOrb = false;
            }
        }
        public void Update() 
        {
            if(IsOnOrb && playerInput.chatch)
            {
                if(Oncollision != null)
                {
                    IsOnOrb = false;
                    playermovement.movingType = State.Chatching;
                    playermovement.StartCoroutine("ChangeStateBead");
                    Oncollision.transform.SetParent(playermovement.transform);
                    Oncollision.GetComponent<Orb>().Chatching(playermovement.transform, targetVec2);
                }
            }
        }
    }
}