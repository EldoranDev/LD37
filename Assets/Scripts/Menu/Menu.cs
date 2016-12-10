using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

using UnityEngine;

class Menu : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ButtonClose()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
