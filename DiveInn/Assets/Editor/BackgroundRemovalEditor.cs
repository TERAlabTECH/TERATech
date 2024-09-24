using UnityEngine;
using UnityEditor;

public class BackgroundRemovalEditor : EditorWindow
{
    private SpriteRenderer spriteRenderer;

    [MenuItem("Tools/Remove Black Background")]
    public static void ShowWindow()
    {
        GetWindow<BackgroundRemovalEditor>("Remove Black Background");
    }

    void OnGUI()
    {
        GUILayout.Label("Remove Black Background from Sprite", EditorStyles.boldLabel);

        // Field to assign the sprite renderer
        spriteRenderer = EditorGUILayout.ObjectField("Sprite Renderer", spriteRenderer, typeof(SpriteRenderer), true) as SpriteRenderer;

        if (spriteRenderer != null && GUILayout.Button("Remove Background"))
        {
            RemoveBlackBackground(spriteRenderer);
        }
    }

    private void RemoveBlackBackground(SpriteRenderer spriteRenderer)
    {
        // Create a new texture from the sprite's texture
        Texture2D texture = spriteRenderer.sprite.texture;
        Texture2D newTexture = new Texture2D(texture.width, texture.height);

        // Get all pixels from the original texture
        Color[] pixels = texture.GetPixels();

        // Modify pixels to make the black ones transparent
        for (int i = 0; i < pixels.Length; i++)
        {
            if(pixels[i].a<1.0f){
                pixels[i]=new Color(0.92f,0.92f, 0.92f, 1f);
            }
        }

        // Apply modified pixels to the new texture
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        // Update the sprite with the new texture
        Rect spriteRect = new Rect(0, 0, newTexture.width, newTexture.height);
    
        spriteRenderer.sprite = Sprite.Create(newTexture, spriteRect, new Vector2(0.5f, 0.5f));

        // Save the new texture as an asset (optional)
        byte[] bytes = newTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/ModifiedTexture.png", bytes);
        AssetDatabase.Refresh();
    }
}
