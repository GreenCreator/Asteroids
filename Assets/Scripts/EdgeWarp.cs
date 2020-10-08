using UnityEngine;

public class EdgeWarp : MonoBehaviour
{

    void Update()
    {
        var onScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        var newPosition = onScreenPosition;

        if (onScreenPosition.x < 0)
        {
            newPosition.x = Screen.width;
        }

        if (onScreenPosition.x > Screen.width)
        {
            newPosition.x = 0;
        }
        if (onScreenPosition.y < 0)
        {
            newPosition.y = Screen.height;
        }
        if (onScreenPosition.y > Screen.height)
        {
            newPosition.y = 0;
        }

        var worldCoordinates = Camera.main.ScreenToWorldPoint(newPosition);
        this.transform.position = new Vector3(worldCoordinates.x, 0, worldCoordinates.z);

    }
}
