using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KitchenViewController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform _canvas;
    private float _speed = 50.0f;
    private float _dragSpeed;
    private float _leftBound;
    private float _righttBound;
    private float _posX;
    void Start()
    {
        _leftBound = -960*_canvas.localScale.x;
        _righttBound = (960-_canvas.rect.width)*_canvas.localScale.x;
        _dragSpeed = _canvas.localScale.x;
        //_canvas.transform.position = new Vector3(_leftBound, 0, 0);
    }

    
    void Update()
    {
        move();
    }

    private void move()
    {
        if (Input.GetKey(KeyCode.A)) moveLeft();
        if (Input.GetKey(KeyCode.D)) moveRight();
        
    }

    private float checkBound(float x)
    {
        if (x > _leftBound) { x = _leftBound; }
        if (x < _righttBound) { x = _righttBound; }
        return x;
    }

    private void moveLeft()
    {
        float newX = checkBound(_canvas.transform.position.x + _speed * Time.deltaTime);
        _canvas.transform.position = new Vector3(newX, _canvas.transform.position.y, _canvas.transform.position.z);
    }

    private void moveRight()
    {
        float newX = checkBound(_canvas.transform.position.x - _speed * Time.deltaTime);
        _canvas.transform.position = new Vector3(newX, _canvas.transform.position.y, _canvas.transform.position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaX = eventData.position.x -_posX;
        float newX = checkBound(_canvas.transform.position.x + deltaX * _dragSpeed);
        _canvas.transform.position = new Vector3( newX, _canvas.transform.position.y, _canvas.transform.position.z);
        _posX = eventData.position.x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _posX = eventData.position.x;
        
    }
}
