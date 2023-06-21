using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenImage : MonoBehaviour
{
    public static int selectedImageIndex;
    public Image largeImage;
    private List<Image> images;

    private void Start()
    {
        //images = ImageLoader(); // Загружаем все изображения с сервера

        // Отображаем выбранное изображение
        //ShowImage(selectedImageIndex);

    }
}
