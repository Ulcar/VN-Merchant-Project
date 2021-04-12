using UnityEngine;
using System.Collections;
using Naninovel;


public class InteractableObject : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    private string script;
    [SerializeField]
    private string label;

    [SerializeField]
    Naninovel.Script scriptFile;


    public string Script { get => script; set => script = value; }
    public string Label { get => label; set => label = value; }


    private Material highlightMaterial;



    private void Start()
    {
        if (scriptFile != null)
        {
            script = scriptFile.name;
        }
        highlightMaterial = GetComponent<Renderer>().material;

    }


    }