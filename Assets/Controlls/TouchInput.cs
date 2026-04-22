using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHeld { get; private set; }
    private Image image;
    private Color defaultColor;
    private void Start()
    {
         image = this.GetComponent<Image>();
        defaultColor = image.color;
    }
    private void Update()
    {
        if (IsHeld)
        {
            image.color = new Color(1f,1f,1f,0.15f);
        }
        else
        {
            image.color = defaultColor;
        }
    }

    public void OnPointerDown(PointerEventData e) => IsHeld = true;
    public void OnPointerUp(PointerEventData e) => IsHeld = false;

    void OnDisable() => IsHeld = false;
}
