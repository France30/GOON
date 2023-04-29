using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    [SerializeField] private float hapticAmplitude = 0.4f;
    [SerializeField] private float hapticDuration = 0.4f;

    private XRBaseController xrController;
    public void SendHaptics()
    {
        xrController.SendHapticImpulse(hapticAmplitude, hapticDuration);
    }

    private void Awake()
    {
        xrController = GetComponent<XRBaseController>();
    }
}
