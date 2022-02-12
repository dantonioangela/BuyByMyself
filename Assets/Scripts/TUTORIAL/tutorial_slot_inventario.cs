using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tutorial_slot_inventario : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private RectTransform icon;
    private Vector2 initialPos;
    [System.NonSerialized] public bool slotEmpty = true;
    public GameObject productInThisSlot;
    [SerializeField] private Texture emptyTexture;
    [SerializeField] private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<RectTransform>();
        //transform.GetComponent<RawImage>().texture = emptyTexture;
        initialPos = icon.anchoredPosition;
        //number.text = "0";
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
                icon.anchoredPosition = initialPos;
                icon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f);
                RemoveProduct();
            }
        }
    }

    public void AddProductInSlot(GameObject product)
    {
        productInThisSlot = product;
        transform.GetComponent<RawImage>().texture = product.GetComponent<icon>().myIcon;
        slotEmpty = false;
    }

    public void RemoveProduct()
    {
        //number.text = (int.Parse(number.text) - 1).ToString();

            transform.GetComponent<RawImage>().texture = emptyTexture;
            slotEmpty = true;
            Camera.main.gameObject.transform.parent.GetChild(1).GetComponent<tutorial_carrello_controller>().RemoveProductFromChart(productInThisSlot);
            transform.parent.parent.parent.GetComponent<tutorial_UI>().RemoveProductFromInventario(productInThisSlot);
            productInThisSlot = null;
    }
}
