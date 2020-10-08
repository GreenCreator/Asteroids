using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int numberOfLives;

    public Image[] lives;

    void Update()
    {
        for (int i = 0; i < lives.Length; i++)
        if (i<numberOfLives)
        {
            lives[i].enabled = true;
        }
        else
            { lives[i].enabled = false; }
        
    }
}
