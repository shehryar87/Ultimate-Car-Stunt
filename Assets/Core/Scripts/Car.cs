using UnityEngine;

public class Car : MonoBehaviour
{
    public CarDataHolder carDataHolder;

    private void Start()
    {
        if (carDataHolder.isUpgraded)
        {
            WheelCollider[] wheelColliders = GetComponentsInChildren<WheelCollider>();
            foreach (WheelCollider wheelCollider in wheelColliders)
            {
                wheelCollider.mass += 20;
            }
            GetComponent<RCC_CarControllerV3>().useDamage = false;
            GetComponent<RCC_CarControllerV3>().maxspeed = 300;
        }
    }
}
