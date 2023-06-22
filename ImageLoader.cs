using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    //[SerializeField] public Image imagePrefab;
    
    [SerializeField] public Image image;
    private Transform imageContainer;
    public List<Image> imageList;
    [SerializeField] public static string imageLink = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int numberToCreate;
    
    void Start()
    {
        StartCoroutine(imageVisualization());
    }

    private IEnumerator imageVisualization(){

        imageList = CreateListImage();

        for(int index = 0; index < imageList.Count; index++){
            image = Instantiate(image, imageContainer);
            //newImage.GetComponent<Image>().sprite = imageList[index].sprite;
            image.sprite = imageList[index].sprite;
        }

        Debug.Log("I create Object and get component 'Sprite'");

        yield return new WaitForEndOfFrame();

        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();

        scrollRect.content.sizeDelta = new Vector2(
            0, 
            imageContainer.childCount * image.GetComponent<RectTransform>().rect.height);

        Debug.Log("I create config to scroll");
        
        for (int i = 0; i < imageContainer.childCount; i++)
        {
            int index = i;
            imageContainer.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ShowImage(index));
        }

        Debug.Log("I create onClick component");
    }

    private void ShowImage(int index)
    {
        // Передаем индекс выбранного изображения для отображения на Сцене 3
        OpenImage.selectedImageIndex = index;
        SceneManager.LoadScene("Photo");
    }


    public Sprite LoadImageFromServer(int index){

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageLink + $"{index}.jpg");
        request.SendWebRequest();

        Debug.Log("I will create request");
        
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Произошла ошибка " + request.error);
            request.Dispose();
            return null;
        }
        else
        {
            Debug.Log("No Error");
            //image = Instantiate(image, transform);
            //image.name = $"Image {i}";
            Debug.Log("Star dowload texture");

            Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.Log("Finish dowload texture");

            Debug.Log("Star create sprite");
            Sprite newSprite = Sprite.Create(
                myTexture, 
                new Rect(0, 0, myTexture.width, myTexture.height),
                new Vector2(0.5f, 0.5f)
                );
            Debug.Log("I finish request");
            return (Sprite)newSprite;
        }
    
        
    }

    private List<Image> CreateListImage(){
        List<Image> tempImageList = new List<Image>();
        Debug.Log("I will create List");
        numberToCreate = 2;
        for(int i = 1; i <=numberToCreate; ++i )
        {
            image.sprite = LoadImageFromServer(i);
            tempImageList.Add(image);
        }
            numberToCreate += 2;
        Debug.Log("I finish create List");
        return tempImageList;
    }
}
