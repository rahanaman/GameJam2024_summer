using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hand;
    [SerializeField] private Transform handPos;
    public void SetSprite(Sprite sprite)
    {
        hand.sprite = sprite;
    }

    public void SetPosition(Vector3 pos)
    {
        handPos.position = new Vector3(pos.x,pos.y,transform.position.z);
    }
    
}
