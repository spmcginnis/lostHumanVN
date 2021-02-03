using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBackgroundImage : MonoBehaviour
{
    public Texture texture;
    ImageController imageController;

    void Start()
    {
        imageController = ImageController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        ImageController.ImageLayer layer = imageController.background;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            layer.StartImageTransition(texture);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            layer.StartImageTransition(newTexture: null);
        }
        

    }
}
