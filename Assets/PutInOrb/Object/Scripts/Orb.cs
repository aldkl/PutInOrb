using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace PutInOrb
{
    public class Orb : MonoBehaviour
    {

        public float duration = 0.5f;
        private Transform playerTransform; // 플레이어의 Transform
        private Vector3 targetPosition; // 목표 위치 (플레이어 머리 위)

        private CircleCollider2D circleCollider;
        private ScaleByYPosition myScaleByYPosition;
        private SpriteRenderer myrenderer;
        private bool EndMove = false;

        public int type;
        public void Chatching(Transform player, Vector3 target)
        {
            playerTransform = player;
            targetPosition = target;
            StartCoroutine(MoveToPlayerHead());
            circleCollider.isTrigger = true;
            myScaleByYPosition.enabled = false;
            myrenderer.sortingOrder = 1;

        }
        private IEnumerator MoveToPlayerHead()
        {
             // 이동 시간
            Vector3 startPosition = transform.localPosition;
            float startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            transform.localPosition = targetPosition;
            EndMove = true;
        }
        private IEnumerator MoveToHole()
        {
            // 이동 시간
            Vector3 startPosition = transform.localPosition;
            float startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            transform.localPosition = targetPosition;
            EndMove = true;
        }

        private void Start()
        {
            duration = GameManager.instance.GetOrbduration;
            circleCollider = GetComponent<CircleCollider2D>();
            myScaleByYPosition = GetComponent<ScaleByYPosition>();
            myrenderer = GetComponent<SpriteRenderer>();
            EndMove = false;
        }
        private void Update()
        {
            if (EndMove) 
            { 
                transform.localPosition = targetPosition;
            }
        }
    }

}