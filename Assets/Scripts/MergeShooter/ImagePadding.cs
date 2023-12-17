using UnityEngine;
using System.IO;

public class ImagePadding : MonoBehaviour
{
    public Texture2D originalImage;

    void Start()
    {
        Texture2D paddedImage = PadImage(originalImage);

        string folderPath = Application.dataPath + "/PaddedImages/";
        string fileName = "paddedImage.png";
        string filePath = folderPath + fileName;

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        byte[] bytes = paddedImage.EncodeToPNG();

        File.WriteAllBytes(filePath, bytes);

        Texture2D savedImage = new Texture2D(1, 1);
        savedImage.LoadImage(bytes);

        Debug.Log("Padded image saved at: " + filePath);
    }

    Texture2D PadImage(Texture2D original)
    {
        int newWidth = original.width;
        int newHeight = original.height;

        if (newWidth % 4 != 0)
        {
            newWidth += 4 - (newWidth % 4);
        }

        if (newHeight % 4 != 0)
        {
            newHeight += 4 - (newHeight % 4);
        }

        if (newWidth == original.width && newHeight == original.height)
        {
            return original;
        }

        Texture2D paddedImage = new Texture2D(newWidth, newHeight);
        paddedImage.SetPixels32(original.GetPixels32());
        paddedImage.Apply();

        return paddedImage;
    }
}
