using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CancelTeleportHintScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Teleport.instance.CancelTeleportHint();
    }
}
