using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot_inventario_controller : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private RectTransform icon;
    private Vector2 initialPos;
    private bool slotEmpty;
    [SerializeField]  private Text number;
    [SerializeField] private Texture emptyTexture;
    [SerializeField] private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<RectTransform>();
        initialPos = icon.anchoredPosition;
        number.text = "1";
        slotEmpty = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!slotEmpty)
        {
            if (icon.anchoredPosition.x < 20 || icon.anchoredPosition.x > 450)
            {
                icon.GetComponent<RawImage>().color = new Color(0f, 0f, 0f);
            }
            else icon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f);

            icon.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!slotEmpty)
        {
            if (icon.anchoredPosition.x > 20 && icon.anchoredPosition.x < 450)
            {
                icon.anchoredPosition = initialPos;
            }
            else
            {
                number.text = (int.Parse(number.text) - 1).ToString();
                if( int.Parse(number.text) == 0)
                {
                    icon.GetComponent<RawImage>().texture = emptyTexture;
                    slotEmpty = true;
                    number.text = "";
                }
            }
        }
    }
}
