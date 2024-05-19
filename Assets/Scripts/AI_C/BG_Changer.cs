using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BG_Changer : MonoBehaviour
{
   [SerializeField] bool isUI = false;
   [SerializeField] bool isQuad = false;

    public void ChangeBG()
    {
        if(isUI)
        {
            var imageName = AI_DataManager.Instance.DataObject.paths.Screen2BG;
            var imagePath = System.IO.Path.Combine(Application.streamingAssetsPath, imageName);

            Texture2D texture = LoadTextureFromFile(imagePath);

            // If the texture is loaded successfully
            if (texture != null)
            {
                // Convert the Texture2D to a Sprite
                Sprite sprite = SpriteFromTexture(texture);

                // Apply the Sprite to the UI Image component
                GetComponent<Image>().sprite = sprite;
            }
        }

        else if(isQuad)
        {
            var imageName = AI_DataManager.Instance.DataObject.paths.CharacterBG;
            var imagePath = System.IO.Path.Combine(Application.streamingAssetsPath, imageName);

            Texture2D texture = LoadTextureFromFile(imagePath);

            if (texture != null)
            {
                GetComponent<MeshRenderer>().material.mainTexture = texture;
            }
        }
    }


    private Texture2D LoadTextureFromFile(string path)
    {
        Texture2D texture = null;

        // Read the file as bytes
        byte[] fileData = System.IO.File.ReadAllBytes(path);

        // Create a new Texture2D and load the image data
        texture = new Texture2D(2, 2);
        if (!ImageConversion.LoadImage(texture, fileData))
        {
            Debug.LogError("Failed to load texture from file data.");
            return null;
        }

        return texture;
    }

    private Sprite SpriteFromTexture(Texture2D texture)
    {
        // Create a new Sprite using the Texture2D
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        return sprite;
    }
}
