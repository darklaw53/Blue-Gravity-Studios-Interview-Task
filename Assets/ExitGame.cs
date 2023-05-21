using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void CloseGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}