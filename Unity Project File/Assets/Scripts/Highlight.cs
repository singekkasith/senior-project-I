using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color color = Color.white;

    [SerializeField]
    private float intensity = 0.2f;

    [SerializeField]
    private List<Material> materials;

    private void Awake(){
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val){
       if (val){
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color * intensity);
            }
       } 
       else{
            foreach (var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
       }
    }
}
