using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChamarStart());
    }

    private void TutorialStart()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    //Corrotina para não travar a transição no início da fase
    IEnumerator ChamarStart()
    {
        yield return new WaitForSeconds(2.5f);
        TutorialStart();
    }

}
