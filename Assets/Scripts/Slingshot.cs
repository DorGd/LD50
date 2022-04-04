using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slingshot : MonoBehaviour
{
    public int multiplier;
    
    private Vector3 _originPosition;
    
    private Vector3 _currentPosition;
    
    private bool _isPressed;

    private List<Rigidbody> _selectedRigidBody;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _selectedRigidBody = new List<Rigidbody>();
    }

    private float AngleBetweenTwoPoints()
    {
        Vector2 positionOnScreen = _selectedRigidBody.First().position;
        
        Vector2 mouseOnScreen = _currentPosition;
        
        return Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
    }
    
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
    
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = _camera.ScreenPointToRay(mousePos);

            var hit = Physics.RaycastAll(ray);
            if (hit.Length > 0)
            {
                _originPosition = hit[0].transform.position;
                transform.position = _originPosition;
                
                foreach (var t in hit)
                {
                    if (t.rigidbody != null)
                    {
                        _selectedRigidBody.Add(t.rigidbody);
                    }
                }
                
                _isPressed = true;
            }
        }
        
        if (_isPressed && Input.GetButton("Fire1"))
        {
            if ( _selectedRigidBody.First() == null)
            {
                _isPressed = false;
                _selectedRigidBody.Clear();
                return;
            }
            _currentPosition = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,Mathf.Abs(_camera.transform.position.z - _selectedRigidBody.First().position.z)));
            var direction = _currentPosition - _originPosition;
            
            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,AngleBetweenTwoPoints()));
            
            Debug.DrawLine(_originPosition, _originPosition + direction, Color.magenta);
        }
        
        if (_isPressed && Input.GetButtonUp("Fire1"))
        {
            SlingHumanToSpace();
        }
    }

    private void SlingHumanToSpace()
    {
        var direction = (_currentPosition - _originPosition) * multiplier;
        foreach (var rb in _selectedRigidBody)
        {
            if (rb != null)
            {
                rb.AddForce(-direction);
            }
        }
        _isPressed = false;
        _selectedRigidBody.Clear();
    }
}
