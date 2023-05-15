using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item")]
public class Item : ScriptableObject
{
    public new string name;
    public int weight;
    public float bounciness;
    public Sprite sprite;
    public Color color = new Color(255, 255, 255, 255);
    public int ID;
}
