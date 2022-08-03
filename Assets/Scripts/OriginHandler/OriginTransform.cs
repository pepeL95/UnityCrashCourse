using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginTransform : MonoBehaviour
{
    [SerializeField] private GameSettingsScriptableObject gameSettings;
    private float initialXPosition;
    private bool moveLeft;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-20.0f, 20.0f), 1.5f, Random.Range(-20.0f, 20.0f));
        initialXPosition = transform.position.x;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameSettings.originSpeed != 0)
            speed = gameSettings.originSpeed;
        
        if (transform.position.x <= initialXPosition - 5)
            moveLeft = false;
        else if (transform.position.x >= initialXPosition + 5)
            moveLeft = true;
        
        if (moveLeft)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        else
            transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
