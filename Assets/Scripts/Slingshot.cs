using System;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public Vector3 originPosition;
    
    public Vector3 currentPosition;

    public LineRenderer lineRenderer;

    public int multiplier;
    
    private bool _isPressed;
    
    float AngleBetweenTwoPoints() {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
         
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        return Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
    }
    
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
    
        if (Input.GetButtonDown("Fire1"))
        {
            originPosition = new Vector3(mousePos.x, mousePos.x, 1);
    
            _isPressed = true;
        }
        
        if (Input.GetButton("Fire1"))
        {
            currentPosition = new Vector3(mousePos.x, mousePos.x, 1);
    
            var direction = (currentPosition - originPosition) / multiplier;
            
            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,AngleBetweenTwoPoints()));

            Debug.Log(direction);
            
            lineRenderer.SetPosition(1, direction);
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            _isPressed = false;
    
            SlingHumanToSpace();
        }
    }

    private void SlingHumanToSpace()
    {
        
    }
}
