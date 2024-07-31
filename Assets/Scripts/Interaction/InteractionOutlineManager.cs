using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOutlineManager : MonoBehaviour
{
    private GunManager rayGun;
    public Outline outline;
    [SerializeField] private bool isDirectGrabbable;

    private void Start()
    {
        outline.enabled = false;

        if (rayGun == null)
        {
            rayGun = GameObject.FindGameObjectWithTag("RayGun").GetComponent<GunManager>();
        }

        if (this.gameObject != rayGun.gameObject)
        {
            outline.OutlineColor = Color.green;
        }
    }

   /* private void Update()
    {

        if (!isDirectGrabbable && !rayGun.isPickedUp)
        {
            outline.OutlineColor = Color.red;
        }
        else if (!isDirectGrabbable && rayGun.isPickedUp)
        {
            outline.OutlineColor = Color.green;
        }
    }*/

    public void ChangeOutlineColor(Color color)
    {
        outline.OutlineColor = color;
    }

    public void OutlineRed()
    {
        outline.OutlineColor = Color.red;
    }

    public void OutlineGreen()
    {
        outline.OutlineColor = Color.green;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
}
