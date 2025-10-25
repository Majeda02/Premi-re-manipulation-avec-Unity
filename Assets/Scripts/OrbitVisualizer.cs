using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitVisualizer : MonoBehaviour
{
    public Transform center;      // le Soleil
    public float radius = 5f; // Le rayon du cercle (Trajectoire)
    [Range(12, 360)] public int segments = 64; // 64 segments pour dessiner le cercle
    public float lineWidth = 0.05f;
    public Material lineMaterial; // Material utilisé

    private LineRenderer lr; // Trace la ligne visible

    void Awake()
    {
        lr = GetComponent<LineRenderer>(); //Récupération  de LineRenderer de l'objet
        SetupLineRenderer(); // Configuration de LineRenderer
        DrawOrbit(); // Dessiner l'orbite
    }

    //Méthode de validation (Appelé automatiquement dans l'editeur Unity) Après chaque modification redessine automatiquement
    void OnValidate()
    {
        if (lr == null) lr = GetComponent<LineRenderer>();
        SetupLineRenderer();
        DrawOrbit(); //redessiner 
    }

    //Méthode de configuration  de LineRenderer
    void SetupLineRenderer()
    {

        lr.loop = true; //boucle de répitition pour faire un cercle fermé LineRender relier dernier point au premier
        lr.positionCount = segments; // nbre de points aue LineRenderer utilisera
        lr.startWidth = lineWidth; 
        lr.endWidth = lineWidth;
        lr.useWorldSpace = true;

        if (lineMaterial != null)  //si un matériau est fourni 
            lr.sharedMaterial = lineMaterial; //Affectation au LineRenderer via sharedMaterial
        else // Sinon
        {
            // Création d'un matériau de secours
            var mat = new Material(Shader.Find("Unlit/Color")); 
            mat.color = new Color(0.8f, 0.8f, 0.8f, 0.7f);
            lr.sharedMaterial = mat; // Assigne ce matériau temporaire au LineRenderer
        }
    }

    public void DrawOrbit()
    {
        if (center == null) return; // centre n'est pas défini ---> return
        Vector3 centerPos = center.position;  //récuperation de la position 
        float step = 2f * Mathf.PI / segments; // le pas entre chaque point de cercle
        for (int i = 0; i < segments; i++) // Boucle pour chaque point
        {
            float a = i * step;
            float x = Mathf.Cos(a) * radius;
            float z = Mathf.Sin(a) * radius;
            Vector3 pos = centerPos + new Vector3(x, 0f, z); //Construire la position finale
            lr.SetPosition(i, pos); //Assigne la position final
        }
    }
}
