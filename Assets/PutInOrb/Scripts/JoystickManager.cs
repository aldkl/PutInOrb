using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PutInOrb
{
    public class JoystickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image imgJoystickBG;
        [SerializeField] private Image imgJoystick;

        private Vector2 posInput;

        public float Horizontal { get { return posInput.x; } }
        public float Vertical { get { return posInput.y; } }

        public bool chatch = false;
        public bool put = false;


        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out posInput))
            {//�巡�װ� �Ͼ�� imgJoystickBG�� �߽����� �������� ��ǥ�� out���ش�

                //��ǥ ������ imgJoystickBG.rectTransform ũ�⿡ ��ʽ�Ų��
                posInput.x = posInput.x / (imgJoystickBG.rectTransform.sizeDelta.x / 2);
                posInput.y = posInput.y / (imgJoystickBG.rectTransform.sizeDelta.y / 2);
                //Debug.Log(posInput.ToString());

                //���� �Ѿ�� ��ֶ������ �����ֱ�
                if (posInput.magnitude > 1.0f)
                {
                    posInput = posInput.normalized;
                }


                //���̽�ƽ �̵�

                imgJoystick.rectTransform.anchoredPosition =
                    new Vector2(posInput.x * imgJoystickBG.rectTransform.sizeDelta.x / 2,
                                posInput.y * imgJoystickBG.rectTransform.sizeDelta.y / 2);

            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            posInput = Vector2.zero;
            imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetChatchDown(BaseEventData eventData)
        {
            chatch = true;
        }
        public void SetChatchUp(BaseEventData eventData)
        {
            chatch = false;
        }

        public void SetPutDown(BaseEventData eventData)
        {
            put = true;
        }
        public void SetPutUp(BaseEventData eventData)
        {
            put = false;
        }

    }
}