using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerToCoralCollider : MonoBehaviour
{
    private Color originalCoralColor;
    public GameObject levelManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected");

        if (other.gameObject.CompareTag("coral"))
        {
            // Attempt to get the SpriteRenderer from the coral object
            SpriteRenderer coralRenderer = other.gameObject.GetComponent<SpriteRenderer>();

            if (coralRenderer != null)
            {
                originalCoralColor = coralRenderer.color;
                coralRenderer.color = Color.white;
                // Start the coroutine to reset the color
                StartCoroutine(ResetColorNextFrames(coralRenderer));
            }

            // Invoke the coral damage function on the LevelManager
            levelManagerObject.GetComponent<LevelManager>().LastimoCoral();
        }
        
    }

    private IEnumerator ResetColorNextFrames(SpriteRenderer coralImage)
    {
        // Show white for 3 frames
        for (int i = 0; i < 3; i++)
        {
            coralImage.color = Color.white;
            yield return null; // Wait for 1 frame
        }

        

        // Show red for 3 frames
        for (int i = 0; i < 3; i++)
        {
            coralImage.color = Color.red;
            yield return null; // Wait for 1 frame
        }

        

        // Show white again for 3 frames
        for (int i = 0; i < 3; i++)
        {
            coralImage.color = new Color32(115, 55, 157, 255);
            yield return null; // Wait for 1 frame
        }



        // Finally, reset to the original color
        coralImage.color = originalCoralColor;
        Debug.Log("Coral color reset to original after 9 frames.");
    }
}
