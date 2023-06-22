using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenImage : MonoBehaviour
{
    public static int selectedImageIndex;
    public Image largeImage;

    private void Start()
    {
        // Отображаем выбранное изображение
        ShowImage(selectedImageIndex);

    }

    public void ShowImage(int index)
    {
        ImageLoader imgObj = new ImageLoader();
        largeImage.sprite = imgObj.LoadImageFromServer(selectedImageIndex);
    }
}
