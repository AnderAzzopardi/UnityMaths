using UnityEngine;

public class PopupWindow : MonoBehaviour
{
    public float widthPercentage = 60f;
    public float heightPercentage = 35f;

    private Rect popupRect;

    private void Start()
    {
        CalculatePopupSizeAndPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(popupRect.x + popupRect.width / 2, popupRect.y + popupRect.height / 2), new Vector3(popupRect.width, popupRect.height));
    }

    private void CalculatePopupSizeAndPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float popupWidth = screenWidth * (widthPercentage / 100f);
        float popupHeight = screenHeight * (heightPercentage / 100f);

        float popupX = (screenWidth - popupWidth) / 2;
        float popupY = (screenHeight - popupHeight) / 2;

        popupRect = new Rect(popupX, popupY, popupWidth, popupHeight);

        // Adjust Unity GUI or transform position based on popupRect values
        // For example, if using Unity GUI:
        // guiRect = new Rect(popupRect.x, popupRect.y, popupRect.width, popupRect.height);
    }
}
