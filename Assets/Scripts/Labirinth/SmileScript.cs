using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmileScript : MonoBehaviour
{
    // [SerializeField] 
    private GameObject _camera;
    [SerializeField] private Rigidbody body;
    [SerializeField] private GameObject cameraAnchor;

    private Vector3 anchorOffset;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource collectSound;
    
    private float forceFactor = 500f;

    private static SmileScript instance = null;

    private void Start()
    {
        if(instance != null)
        {
            // Цей код викликається якщо спавниться новий ГО у новій сцені, але є збережений об'єкт (instance) перенесений з попередньої сцени.
            // Треба перенести з нього потрібні характеристики та видалити його, перейшовши на роботу з "місцевим" ГО
            transform.position += new Vector3(0, instance.transform.position.y, 0);

            if (SceneManager.GetActiveScene().name.Equals("SolarSystem"))
            {
                StartCoroutine("ReturnToMaze");
            }

            Destroy(instance.gameObject);
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);

        _camera = Camera.main.gameObject;
        body = GetComponent<Rigidbody>();
        anchorOffset = transform.position - cameraAnchor.transform.position;
        backgroundMusic.Play();
    }

    private void Update()
    {
        float kh = Input.GetAxis("Horizontal");
        float kv = Input.GetAxis("Vertical");

        Vector3 right = _camera.transform.right;
        Vector3 forward = _camera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 forceDirection = // new Vector3(kh, 0, kv);
            kh * right + kv * forward;

        body.AddForce(forceFactor * Time.deltaTime * forceDirection.normalized);

        cameraAnchor.transform.position = this.transform.position - anchorOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            collectSound.Play();
        }
    }

    private IEnumerator ReturnToMaze()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Labirinth");
    }
}
