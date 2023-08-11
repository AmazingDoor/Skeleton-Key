using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ScaredHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool scared = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float blur = GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>().focalLength.value;
        float vignette = GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().intensity.value;
        if (scared)
        {
            if (blur < 20){GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>().focalLength.value += 0.2f;}
            if (vignette < .52) { GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().intensity.value += 0.005f; }

        }
        else
        {

            if (blur > 1){GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>().focalLength.value -= 0.1f;}
            if(vignette > 0) {GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().intensity.value -= .00125f;}
        }
        
    }

    public void setScared(bool b)
    {
        scared = b;
    }
}
