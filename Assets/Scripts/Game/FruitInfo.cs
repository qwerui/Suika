using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FruitInfo", menuName = "Infos/FruitInfo", order = 0)]
public class FruitInfo : ScriptableObject 
{
    public List<float> radius;
    public List<Sprite> sprites;
    public List<float> spriteRaidus;
}
