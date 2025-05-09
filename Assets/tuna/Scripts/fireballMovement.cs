using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public Transform character;     // Karakterin transform'u
    public float speed = 2f;        // Sal�n�m h�z�
    public float distance = 0.5f;   // Sal�n�m mesafesi
    public Vector3 offset;          // Karaktere g�re ate� topunun konumu (�rne�in yukar�da, yan�nda vs)

    void Update()
    {
        if (character == null)
            return;

        // Karakterin pozisyonuna g�re sal�n�m merkezi
        Vector3 basePosition = character.position + offset;

        // �leri-geri sal�n�m (�rne�in X ekseninde)
        float offsetX = Mathf.Sin(Time.time * speed) * distance;

        // Yeni pozisyon
        transform.position = basePosition + new Vector3(offsetX, 0f, 0f);
    }
}

