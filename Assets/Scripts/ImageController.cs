using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public static ImageController instance;
    public ImageLayer background = new ImageLayer();

    void Awake(){
        instance = this;
    }

    [System.Serializable] // System.Serializable (?)
    public class ImageLayer
    {
        public GameObject panel;
        public GameObject imageObject;
        public RawImage activeImage;
        [HideInInspector]
        public List<RawImage> imageList = new List<RawImage>();
        public bool isBusy {get {return transitionCR != null;}}
        Coroutine transitionCR = null; 

        IEnumerator ImageTransition(Texture newTexture, float speed = 1f)
        {
            if (newTexture != null)
            {
                foreach (RawImage image in imageList)
                {
                    if (image.texture == newTexture)
                    {
                        activeImage = image;
                        break;
                    }
                }

                if (activeImage == null || activeImage.texture != newTexture)
                {
                    CloneImageObject();
                    activeImage.texture = newTexture;
                    activeImage.color = ImageController.SetAlpha(activeImage.color, 0f);
                }
            }
            else 
            {
                activeImage = null;
            }

            while (ImageController.TransitionImage(ref activeImage, ref imageList, speed))
            {
                yield return new WaitForEndOfFrame();
            }

            StopImageTransition();
        }

        void StopImageTransition()
        {
            if (isBusy) { ImageController.instance.StopCoroutine(transitionCR); }
            transitionCR = null;
        }

        public void StartImageTransition(Texture newTexture = null, float speed = 2f)
        {
            if (activeImage != null && activeImage.texture == newTexture) { return; }
            StopImageTransition();
            transitionCR = ImageController.instance.StartCoroutine(ImageTransition(newTexture, speed));
        }

        void CloneImageObject()
        {
            GameObject clone = Instantiate(imageObject, parent: panel.transform) as GameObject; 
            clone.SetActive(true);
            RawImage newImage = clone.GetComponent<RawImage>();
            activeImage = newImage;
            imageList.Add(newImage);
        }
    }

    public static bool TransitionImage(ref RawImage activeImage, ref List<RawImage> imageList, float speed = 2f)
    {
        bool didImageChange = false;
        speed *= Time.deltaTime;

        for (int i = imageList.Count - 1; i >= 0; i-- )
        {
            RawImage image = imageList[i];
            if (image == activeImage)
            {
                if (image.color.a < 1f)
                {
                    image.color = SetAlpha(image.color, Mathf.MoveTowards(image.color.a, 1f, speed));
                    didImageChange = true;
                }
            }
            else
            {
                if (image.color.a > 0)
                {
                    image.color = SetAlpha(image.color, Mathf.MoveTowards(image.color.a, 0f, speed));
                    didImageChange = true;
                }
                else
                {
                    imageList.RemoveAt(i);
                    Destroy(image.gameObject);
                }
            }
        }
        return didImageChange;
    }
    // overload TransitionImage with Image type in place of RawImage type

    public static Color SetAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }




}
