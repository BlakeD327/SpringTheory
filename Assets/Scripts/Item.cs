using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item")]
public class Item : ScriptableObject
{
    public new string name;
    public int weight;
    public float bounciness;
    public Sprite img;
    public Color color;
    public int maxBounce;
    public int ID;
}
