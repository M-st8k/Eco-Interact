using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    public Texture2D mainTexture;
    public Texture2D stencilTexture;

    private Material material;

    void Start()
    {
        // Get the Renderer component attached to the GameObject
        Renderer renderer = GetComponent<Renderer>();

        // Create a new material instance using the custom shader
        material = new Material(Shader.Find("Custom/CircularStencilShader"));

        // Assign the main texture and stencil texture to the shader properties
        material.SetTexture("_MyMainTex", mainTexture);
        material.SetTexture("_MyStencilTex", stencilTexture);

        // Set the material to the Renderer
        renderer.sharedMaterial = material;
    }

    void Update()
    {
        // The rest of your update code, if any...
    }

    // Any other methods you want to define for this script...
}
