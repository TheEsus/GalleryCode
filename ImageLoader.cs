using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] public GameObject imagePrefab;
    
    [SerializeField] public Image image;
    private Transform imageContainer;
    public Image[] imageMas = new Image[66];
    public List<Image> imageList;
    private string imageLink = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int numberToCreate;
    
    void Start()
    {
            
        StartCoroutine(LoadImage());
        StopCoroutine(LoadImage());
        
    }

    private IEnumerator LoadImage(){
        imageList = (List<Image>)LoadImageFromServer();
        for(int index = 0; index < imageList.Count; index++){
            GameObject newImage = Instantiate(imagePrefab, imageContainer);
            newImage.GetComponent<Image>().sprite = imageList[index].sprite;
        }

        yield return new WaitForEndOfFrame();

        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(0, imageContainer.childCount * imagePrefab.GetComponent<RectTransform>().rect.height);

        
        for (int i = 0; i < imageContainer.childCount; i++)
        {
            int index = i;
            imageContainer.GetChild(i).GetComponent<Button>().onClick.AddListener(() => OpenImage(index));
        }
    }

    private void OpenImage(int index)
    {
        // Передаем индекс выбранного изображения для отображения на Сцене 3
        //OpenImage.selectedImageIndex = index;
        //SceneManager.LoadScene("Photo");
    }

    private UnityWebRequest RequestServer(string ImgaeLink){
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageLink);
        //yield return request.SendWebRequest(); 
        return request;
    }
    private List<Image> LoadImageFromServer(){
        numberToCreate = 2;
        for(int i = 1; i <=numberToCreate; ++i ){
            //UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageLink + $"{i}.jpg");
            //yield return request.SendWebRequest();
            var request = RequestServer(imageLink + $"{i}.jpg");
            request.SendWebRequest();

            if(request.isNetworkError || request.isHttpError){
                Debug.Log("Произошла ошибка " + request.error);
                break;
            }
            else{
                Debug.Log("No Error");
                //image = Instantiate(image, transform);
                //image.name = $"Image {i}";
                Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            
                Sprite newSprite = Sprite.Create(
                    myTexture, 
                    new Rect(0, 0, myTexture.width, myTexture.height),
                    new Vector2(0.5f, 0.5f));
                
                image.sprite = newSprite;
                imageList.Add(image);
            }
            request.Dispose();
            
            numberToCreate += 2;
        }
        return imageList;
        
    }

}
