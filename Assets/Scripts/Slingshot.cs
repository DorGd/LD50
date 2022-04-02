using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slingshot : MonoBehaviour
{
    public int multiplier;
    
    private Vector3 _originPosition;
    
    private Vector3 _currentPosition;
    
    private bool _isPressed;

    private Rigidbody _selectedRigidBody;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    float AngleBetweenTwoPoints() {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = _selectedRigidBody.position;
         
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = _currentPosition;
        
        return Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
    }
    
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
    
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                _selectedRigidBody = hit.rigidbody;
                _originPosition = hit.transform.position;
                _isPressed = true;
            }

        }
        
        if (_isPressed && Input.GetButton("Fire1"))
        {
            _currentPosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Mathf.Abs(_camera.transform.position.z - _selectedRigidBody.position.z)));
            var direction = _currentPosition - _originPosition;
            
            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,AngleBetweenTwoPoints()));
            
            Debug.DrawLine(_originPosition, _originPosition + direction, Color.magenta);
        }
        
        if (_isPressed && Input.GetButtonUp("Fire1"))
        {
            _isPressed = false;
            SlingHumanToSpace();
        }
    }

    private void SlingHumanToSpace()
    {
        var direction = (_currentPosition - _originPosition) * multiplier;
        _selectedRigidBody.AddForce(-direction);
    }
    
}
