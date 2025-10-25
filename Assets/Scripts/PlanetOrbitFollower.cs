using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbitFollower : MonoBehaviour
{
    [Tooltip("L'OrbitVisualizer fixe (Orbit2, Orbit3 ou Orbit4)")]
    public OrbitVisualizer orbitVisualizer; // fait Appel au script 2 qui visualiser le chemin de mouvement de chaque planète

    [Tooltip("Vitesse angulaire (radians/sec). Ajuster pour ralentir/accélérer.")]
    public float angularSpeed = 0.5f;

    [Tooltip("Angle initial en degrés (pour répartir les planètes)")]
    public float initialAngleDeg = 0f;

    private float angleRad;

    void Start()  //Méthode Start   
    //Vérifier si un orbitVisualizer(Trajectoire) est assigné 
    {
        if (orbitVisualizer == null)
        {
            Debug.LogWarning($"{name}: pas d'OrbitVisualizer assigné !");
            enabled = false;
            return; // Arret de script
        }

        // converti l'angle initial en radians
        angleRad = initialAngleDeg * Mathf.Deg2Rad;

        // position initiale sur l'orbite  ( retour à sa place initiale)
        UpdatePosition();
    }


    void Update()  //Méthode Update
    {
        // incrémente l'angle (angularSpeed en radians/sec)
        angleRad += angularSpeed * Time.deltaTime;

        UpdatePosition(); //Déplacement à la nouvelle position 
    }

    void UpdatePosition()  // Méthode update position
    {
        Transform center = orbitVisualizer.center; //récupération de centre 
        float radius = orbitVisualizer.radius; // récupération de rayon
        if (center == null) return;
         //Calcule de position sur le cercle
        float x = Mathf.Cos(angleRad) * radius;
        float z = Mathf.Sin(angleRad) * radius;

        //Place chque planete à la position autour du centre
        transform.position = center.position + new Vector3(x, 0f, z);

        // orienter la planète vers la tangente pour effet de direction (optionnel)
        Vector3 tangent = new Vector3(-Mathf.Sin(angleRad), 0f, Mathf.Cos(angleRad));
        if (tangent.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);
    }
}
