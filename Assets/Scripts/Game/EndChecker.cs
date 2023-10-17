using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
public class EndChecker : MonoBehaviour
{
    BoxCollider2D box;
    SpriteRenderer sprite;
    Color color;
    public UIManager ui;

    Sequence endSequence;
    const string FRUIT = "Fruit";

    private void Awake() 
    {
        TryGetComponent(out box);
        TryGetComponent(out sprite);
        color = new Color(0.8f, 0.16f, 0.16f, 1.0f);
        endSequence = DOTween.Sequence();

        endSequence
        .SetAutoKill(false)
        .Append(sprite.DOFade(0.2f, 0.5f).SetLoops(6, LoopType.Yoyo))
        .OnComplete(()=>{
            sprite.color = color;
            ui.ShowGameOver();
        });
    }

    private void Start() 
    {
        GameManager.GetManager().OnGameStart += () => box.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag(FRUIT))
        {
            other.TryGetComponent(out Fruit fruit);

            if (fruit.IsDropped)
            {
                endSequence.Restart();
                box.enabled = false;
                GameManager.GetManager().OnGameEnd.Invoke();
            }
        }    
    }
}
