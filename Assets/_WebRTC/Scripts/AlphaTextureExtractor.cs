using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

public class AlphaTextureExtractor : MonoBehaviour
{
    [SerializeField] private RenderTexture inputTexture;

    // Start is called before the first frame update
    void Start()
    {
        var textureChannels = (int)GraphicsFormatUtility.GetComponentCount(inputTexture.graphicsFormat);

        FormatSwizzle alphaChannel = GraphicsFormatUtility.GetSwizzleA((GraphicsFormat)inputTexture.format);

        // Texture2D alphaTexture = new Texture2D(inputTexture.width, inputTexture.height, TextureFormat.RGB24);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
