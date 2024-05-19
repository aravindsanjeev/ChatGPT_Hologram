using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCard_Manager : MonoBehaviour
{
    [SerializeField] Material Tag;
    [SerializeField] Material TagHolder;
    [SerializeField] Texture  DummyTex;
    public void OnDataLoaded_CallBack()
    {
        Debug.Log("ID Card manager triggered");

        var idCardIconName = AI_DataManager.Instance.DataObject.paths.IDCardIcon;
        var idCardName = AI_DataManager.Instance.DataObject.paths.IDCardTexture;
        var idCardIcon_imagePath = System.IO.Path.Combine(Application.streamingAssetsPath, idCardIconName);
        var idCard_imagePath = System.IO.Path.Combine(Application.streamingAssetsPath, idCardName);

        Texture2D idCardIconTexture = LoadTextureFromFile(idCardIcon_imagePath);
        Texture2D idCardTexture = LoadTextureFromFile(idCard_imagePath);

        if (idCardIconTexture != null)
        {
            TagHolder.mainTexture = idCardIconTexture;
        }

        if(idCardTexture!=null)
        {
            Tag.mainTexture = idCardTexture;

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

    private void OnDestroy()
    {
        TagHolder.mainTexture = DummyTex;
        Tag.mainTexture = DummyTex;
    }
}
