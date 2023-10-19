using UnityEngine;

[System.Serializable]
public class GradientExporter : MonoBehaviour
{
    public Gradient gradient;

    private Gradient previousGradient;

    private void OnValidate()
    {
        // Check if the gradient has changed
        if (!gradient.Equals(previousGradient))
        {
            SaveGradientAsPNG();
            previousGradient = new Gradient();
            previousGradient.SetKeys(gradient.colorKeys, gradient.alphaKeys);
        }
    }

    private void SaveGradientAsPNG()
    {
        Texture2D texture = new Texture2D(256, 1);
        for (int x = 0; x < 256; x++)
        {
            float t = x / 255.0f;
            Color color = gradient.Evaluate(t);
            texture.SetPixel(x, 0, color);
        }
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Shading/Gradient.png", bytes);
    }
}