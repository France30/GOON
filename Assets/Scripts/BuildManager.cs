using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildManager : Singleton<BuildManager>
{
    private bool isVRBuild = false;
    private bool isPCBuild = true ;

    public bool IsVRBuild { get { return isVRBuild; } }
    public bool IsPCBuild { get { return isPCBuild; } }

    public void VRSelected()
    {
        isVRBuild = true;
        isPCBuild = false;
    }

    public void PCSelected()
    {
        isVRBuild = false;
        isPCBuild = true;
    }

    public void GetMainMenuBuild()
    {
        if (IsPCBuild)
            SceneManager.LoadScene("PC Main Menu");
        else if (IsVRBuild)
            SceneManager.LoadScene("VR Main Menu");
    }

    public void ToggleBuildPause()
    {
        switch(GameController.Instance.IsGamePaused || GameController.Instance.IsGameOver)
        {
            case true:
                if (IsPCBuild) Cursor.lockState = CursorLockMode.Confined;
                if (IsVRBuild) GameObject.FindObjectOfType<ActionBasedContinuousTurnProvider>().enabled = false;
                break;
            case false:
                if (IsPCBuild) Cursor.lockState = CursorLockMode.Locked;
                if (IsVRBuild) GameObject.FindObjectOfType<ActionBasedContinuousTurnProvider>().enabled = true;
                break;
        }
    }

    public void GetGameBuild()
    {
        if (IsPCBuild)
            GetPCBuild();
        else if (IsVRBuild)
            GetVRBuild();
    }

    public override void Awake()
    {

        base.Awake();
        #if UNITY_ANDROID
            VRSelected();
        #elif UNITY_STANDALONE_WIN
            PCSelected();
        #endif
    }

    private void GetVRBuild()
    {
        if(GameObject.Find("VRBuild"))
            GameObject.Find("VRBuild").SetActive(true);

        if (GameObject.Find("PCBuild"))
            GameObject.Find("PCBuild").SetActive(false);
    }

    private void GetPCBuild()
    {
        if (GameObject.Find("VRBuild"))
            GameObject.Find("VRBuild").SetActive(false);

        if (GameObject.Find("PCBuild"))
            GameObject.Find("PCBuild").SetActive(true);
    }
    
}
