using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OLDTVScreen : MonoBehaviour {
    public Material tvMaterialScreen;

    public float screenSaturation = 0.0f;

    public Texture chromaticAberrationPattern;
    public float chromaticAberrationMagnetude = 0.015f;

    public Texture noisePattern;
    public float noiseMagnetude = 0.1f;

    public Texture staticPattern;
    public Texture staticMask;
    public float staticMagnetude = 0.015f;

    void OnRenderImage( RenderTexture source, RenderTexture destination ) {
        float screenHorizontal = 1.0f;
        bool isMobile = false;

        #if ( UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 )
        isMobile = true;
        #endif

        if ( Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight ) {

            if ( isMobile ) {
                screenHorizontal = 0.0f;
            }

            tvMaterialScreen.SetFloat( "_ScreenHorizontal", screenHorizontal );
        } else {

            if ( isMobile ) {
                screenHorizontal = 1.0f;
            }

            tvMaterialScreen.SetFloat( "_ScreenHorizontal", screenHorizontal );
        }

        tvMaterialScreen.SetFloat( "_Saturation", screenSaturation );

        tvMaterialScreen.SetTexture( "_ChromaticAberrationTex", chromaticAberrationPattern );
        tvMaterialScreen.SetFloat( "_ChromaticAberrationMagnetude", chromaticAberrationMagnetude );

        tvMaterialScreen.SetTexture( "_NoiseTex", noisePattern );
        tvMaterialScreen.SetTextureOffset( "_NoiseTex", new Vector2( Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) ) );
        tvMaterialScreen.SetFloat( "_NoiseMagnetude", noiseMagnetude );

        tvMaterialScreen.SetTexture( "_StaticTex", staticPattern );
        tvMaterialScreen.SetTextureOffset( "_StaticTex", new Vector2( Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) ) );
        tvMaterialScreen.SetTexture( "_StaticMask", staticMask );
        tvMaterialScreen.SetFloat( "_StaticMagnetude", staticMagnetude );

        Graphics.Blit( source, destination, tvMaterialScreen );
    }
}
