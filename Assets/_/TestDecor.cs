using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float startSpeed = 2f;          // Vitesse initiale
    public float acceleration = 0.1f;      // Accélération progressive
    public float imageWidth = 20f;         // Largeur de l'image en unités Unity

    private float currentSpeed;

    void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        // Augmenter la vitesse avec le temps
        currentSpeed += acceleration * Time.deltaTime;

        // Déplacement vers la gauche
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        // Réinitialisation de position pour la boucle
        if (transform.position.x <= -imageWidth)
        {
            transform.position += new Vector3(imageWidth * 2f, 0f, 0f);
        }
    }
}
