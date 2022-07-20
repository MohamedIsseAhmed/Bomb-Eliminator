using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampJoystic : MonoBehaviour
{
    public static ClampJoystic instance;    
    [SerializeField] private float xPixel;
    [SerializeField] private float yPixel;
  
    private bool canDrag = false;
    public bool CanDrag { get { return canDrag; } private set { } }
    private RectTransform rectTransform;
    private Vector2 anchoredPosition;

    [SerializeField] private float maxXScreen = 1920f;
    [SerializeField] private float maxYScreen = 1820;
    [SerializeField] private float minYClampScreen = 100;
    [SerializeField] private float maxYClampScreen = 1820;
    
    private void Awake()
    {
        instance = this;
        rectTransform = transform.Find("Background").GetComponent<RectTransform>();
    }
  
    void Update()
    {

        if (Input.mousePosition.x > Screen.width - xPixel)
        {
            float v = Input.mousePosition.x - (Screen.width - xPixel);
            print(v);
            HandleJosytickOutOfScreen(Input.mousePosition.x, true, Input.mousePosition.y);
        }
        //else if (Input.mousePosition.x < xPixel)
        //{
        //    HandleJosytickOutOfScreen(xPixel, true);
        //}
        //else if (Input.mousePosition.y > Screen.height - yPixel)
        //{
        //    HandleJosytickOutOfScreen(rectTransform.anchoredPosition.x, false, maxYScreen);
        //}
        //else if (Input.mousePosition.y < yPixel)
        //{
        //    HandleJosytickOutOfScreen(rectTransform.anchoredPosition.x, false, yPixel);
        //}
        else
        {
            canDrag = true;
        }
        
    }

    private float ClampValues(float value, float a,float b)
    {
        return Mathf.Clamp(value, a, b);
    }
    private void HandleJosytickOutOfScreen(float x,bool isClampedY, float y = 0)
    {
        anchoredPosition=rectTransform.anchoredPosition;
        //if (isClampedY)
        //{
        //    anchoredPosition.x = x;
        //    anchoredPosition.y = ClampValues(anchoredPosition.y, minYClampScreen, maxYClampScreen);
        //}
        //else
        //{
        //    anchoredPosition.x = x;
        //    anchoredPosition.y = y;
            
        //}
        anchoredPosition.x = x;
        anchoredPosition.y = y;

        rectTransform.anchoredPosition=anchoredPosition;
    }
}
