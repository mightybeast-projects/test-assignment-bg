using UnityEngine;

public class PauseButtonBehaviour : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}