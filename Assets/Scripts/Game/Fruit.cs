using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Fruit : MonoBehaviour
{
    int orderNumber;
    int level;
    SpriteRenderer sprite;
    IObjectPool<Fruit> objectPool;
    Rigidbody2D rigid;
    FruitInfo info;
    public AudioClip combineClip;
    
    bool bIsDropped = false;
    public bool IsDropped {get{return bIsDropped;}}
    bool bIsReleased = false;

    const string FRUITTAG = "Fruit";
    const string FLOORTAG = "Floor";

    public void DisableGravity()=>rigid.gravityScale = 0;
    public void EnableGravity()=>rigid.gravityScale = 1;

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        TryGetComponent(out rigid);
        GameManager.GetManager().OnRequsetNewGame += () => 
        {
            if(!bIsReleased)
            {
                objectPool.Release(this);
                bIsReleased = true;
            }
        };

    }

    private void OnEnable() 
    {
        bIsDropped = false;
        bIsReleased = false;
        transform.SetPositionAndRotation(transform.position, Quaternion.identity);
    }

    public void Init(int orderNumber, int level, FruitInfo info, IObjectPool<Fruit> pool)
    {
        this.orderNumber = orderNumber;
        this.level = level;
        this.info = info;
        transform.localScale = Vector3.one * info.radius[level]/2.0f;
        sprite.sprite = info.sprites[level];
        sprite.transform.localScale = Vector3.one * info.spriteRaidus[level];
        objectPool = pool;
    }

    public void Move(Vector2 destination)
    {
        transform.position = destination;
    }

    public void LevelUp()
    {
        level++;
        GameManager.GetManager().OnFruitCombined.Invoke(Mathf.Max(1, level*(level+1)/2));
        SoundQueue.GetSoundQueue().PlaySFX(combineClip);

        if (level > 10)
        {
            objectPool.Release(this);
        }
        else
        {
            transform.localScale = Vector3.one * info.radius[level] / 2.0f;
            sprite.sprite = info.sprites[level];
            sprite.transform.localScale = Vector3.one * info.spriteRaidus[level];
        }

    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        
        if(other.gameObject.CompareTag(FRUITTAG))
        {
            Fruit otherFruit = other.gameObject.GetComponent<Fruit>();

            if(!bIsDropped)
            {
                bIsDropped = true;
                GameManager.GetManager().OnFruitDropped.Invoke();
            }

            if(otherFruit.level == level && otherFruit.orderNumber < orderNumber)
            {
                if(!bIsReleased)
                {
                    bIsReleased = true;
                    otherFruit.LevelUp();
                    objectPool.Release(this);
                }
            }
        }
        else if(other.gameObject.CompareTag(FLOORTAG))
        {
            if(!bIsDropped)
            {
                bIsDropped = true;
                GameManager.GetManager().OnFruitDropped.Invoke();
            }
        }
    }
}
