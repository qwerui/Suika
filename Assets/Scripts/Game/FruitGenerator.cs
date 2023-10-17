using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class FruitGenerator : MonoBehaviour
{
    public FruitInfo info;
    public Fruit fruitPrefab;
    public Image nextImage;

    IObjectPool<Fruit> pool;
    int nextLevelCache;
    int orderNumber;
    Fruit currentFruit;

    private void Awake() 
    {
        pool = new ObjectPool<Fruit>(CreateFruit, GetFruit, ReleaseFruit, DestroyFruit, true, 30, 100);
        nextLevelCache = Random.Range(0, 5); //최초 시작을 위한 초기화
    }
    
    private void Start() 
    {
        GameManager.GetManager().OnGameStart += () => orderNumber = 0;
        GameManager.GetManager().OnGameStart += GenerateFruit;
        GameManager.GetManager().OnFruitDropped += GenerateFruit;
    }

    void GenerateFruit()
    {
        Fruit fruit = pool.Get();
        currentFruit = fruit;
        fruit.Init(++orderNumber, nextLevelCache, info, pool);
        nextLevelCache = Random.Range(0, 5);
        nextImage.sprite = info.sprites[nextLevelCache];
    }

    public Fruit GetCurrentFruit() => currentFruit;

    Fruit CreateFruit()
    {
        return Instantiate(fruitPrefab);
    }

    void GetFruit(Fruit fruit)
    {
        fruit.DisableGravity();
        fruit.transform.localPosition = transform.localPosition;
        fruit.gameObject.SetActive(true);
    }

    void ReleaseFruit(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);
    }

    void DestroyFruit(Fruit fruit)
    {
        Destroy(fruit.gameObject);
    }
}
