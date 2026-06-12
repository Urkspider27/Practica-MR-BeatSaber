using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public CubeSpawnerAndScore gameManager;

    void OnCollisionEnter(Collision collision)
    {
        // Si el cubo choca contra las paredes del escenario, se destruye sin sumar puntos
        if (collision.gameObject.name == "Pared" || collision.gameObject.name == "Pared (1)")
        {
            Destroy(gameObject);
        }
        // Si choca contra cualquier otra cosa (nuestras manos), suma punto y lo rompemos
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Cube"))
        {
            gameManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}