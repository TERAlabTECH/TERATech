using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBackground : MonoBehaviour
{
    void Start()
    {
        // Get the SpriteRenderer component
        SpriteRenderer inputTex = transform.GetComponent<SpriteRenderer>();

        // Create a new texture from the sprite's texture
        Texture2D texture = inputTex.sprite.texture;
        Texture2D newTexture = new Texture2D(texture.width, texture.height);

        // Get all pixels from the original texture
        Color[] pixels = texture.GetPixels();

        // Modify pixels to make the black ones transparent
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].r < 0.4f && pixels[i].g < 0.4f && pixels[i].b < 0.4f)
            {
                // Set pixel to transparent
                pixels[i] = new Color(0, 0, 0, 0);
            }
        }

        // Apply modified pixels to the new texture
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        // Update the sprite with the new texture
        inputTex.sprite = Sprite.Create(newTexture, inputTex.sprite.rect, new Vector2(0.5f, 0.5f));
    }
}
