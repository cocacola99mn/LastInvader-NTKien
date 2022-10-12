using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform barTransform;
    public Image healthFill;
    public float curHealth;
    public float maxHealth;

    private void LateUpdate()
    {
        FreezeRotation();
    }

    public void FreezeRotation()
    {
        barTransform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
