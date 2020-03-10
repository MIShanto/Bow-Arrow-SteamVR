using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour , IDamageable
{
    public void Damage(int amount)
    {
        TurnRed();
    }

    private void TurnRed()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material.color = Color.red;
    }
}
