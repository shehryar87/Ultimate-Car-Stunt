using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CarDataHolder", menuName = "Scriptables/Car Data Holder")]
public class CarDataHolder : ScriptableObject
{
    public string carName;
    public int carPrice;
    public int handeling;
    public int speed;
    public bool isUnlocked;
    public Sprite sprite;
    public int rewardedAdsCount;
    public bool damage;
    public bool isUpgraded;
}
