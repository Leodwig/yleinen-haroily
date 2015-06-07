using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OLDTVTube : MonoBehaviour {
    public Material tvMaterialTube;

    public Texture scanlinePattern;
    public bool scanlineCountAuto = false;
    public int scanlineCount = 320;
    public float scanlineBrightMin = 0.75f;
    public float scanlineBrightMax = 1.0f;
    // scanline min/max
    public Texture mask;
    public Texture reflex;
    public float reflexMagnetude = 0.5f;
    public float radialDistortion = 0.2f;

    public delegate void RepaintAction();

    public event RepaintAction WantRepaint;

    private void Repaint() {
        if ( WantRepaint != null ) {
            WantRepaint();
        }
    }

    void OnRenderImage( RenderTexture source, RenderTexture destination ) {
        float screenHorizontal = 1.0f;
        bool isMobile = false;

        #if ( UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 )
        isMobile = true;
        #endif

        tvMaterialTube.SetTexture( "_ScanLine", scanlinePattern );

        if ( scanlineCountAuto )
            scanlineCount = Screen.height;

        tvMaterialTube.SetFloat( "_ScanLineCount", scanlineCount / 2 );

        tvMaterialTube.SetFloat( "_ScanLineMin", scanlineBrightMin );
        tvMaterialTube.SetFloat( "_ScanLineMax", scanlineBrightMax );

        if ( Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight ) {
            // DEITADO
            if ( isMobile ) {
                screenHorizontal = 0.0f;
                //if( scanlineCountAuto )
                //    tvMaterialTube.SetFloat( "_ScanLineCount", Screen.height / 4.0f );
            } else {
                //if ( scanlineCountAuto )
                //    tvMaterialTube.SetFloat( "_ScanLineCount", Screen.height / 2.0f );
            }

            tvMaterialTube.SetFloat( "_ScreenHorizontal", screenHorizontal );

        } else {    
            // PÉ (PADRÃO)
            if ( isMobile ) {
                screenHorizontal = 1.0f;
                //if ( scanlineCountAuto )
                //    tvMaterialTube.SetFloat( "_ScanLineCount", Screen.height / 4.0f );
            } else {
                //if ( scanlineCountAuto )
                //    tvMaterialTube.SetFloat( "_ScanLineCount", Screen.height / 2.0f );
            }

            tvMaterialTube.SetFloat( "_ScreenHorizontal", screenHorizontal );
        }

        tvMaterialTube.SetTexture( "_Mask", mask );
        tvMaterialTube.SetTexture( "_Reflex", reflex );
        tvMaterialTube.SetFloat( "_ReflexMagnetude", reflexMagnetude );
        tvMaterialTube.SetFloat( "_Distortion", radialDistortion );

        Graphics.Blit( source, destination, tvMaterialTube );
    }
}