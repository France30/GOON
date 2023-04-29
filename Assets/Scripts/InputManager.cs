using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("VR Controls")]
    [SerializeField] private InputActionReference XRTogglePause = null;

    [Header("PC Controls")]
    [SerializeField] private KeyCode pauseKeyCode;

    public bool PauseButton { get; private set; }

    public override void Awake()
    {
        base.Awake();
        PauseButton = false;
    }

    private void Update()
    {
        if (BuildManager.Instance.IsVRBuild)
            GetVRInput();

        else if (BuildManager.Instance.IsPCBuild)
            GetPCInput();
    }

    private void GetVRInput()
    {
        PauseButton = XRTogglePause.action.WasPressedThisFrame();
    }

    private void GetPCInput()
    {
        PauseButton = Input.GetKeyDown(pauseKeyCode);
    }
}
