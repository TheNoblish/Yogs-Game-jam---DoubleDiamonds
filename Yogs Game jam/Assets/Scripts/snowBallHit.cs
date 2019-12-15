using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBallHit : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        Debug.Log("test");
        AudioClip audioClip = GetRandomClip();
        audioSource.PlayOneShot(audioClip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    GameObject package;
    // Start is called before the first frame update
    void Start()
    {
        package = GameObject.Find("package");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Step();
        if (other.gameObject.CompareTag("gull"))
        {
            if (other.gameObject.GetComponent<FlyingEnemy>().gotPackage)
            {
                package.transform.position = new Vector3(transform.position.x,transform.position.y-.5f,0f);
                package.GetComponent<SpriteRenderer>().enabled = true;
                package.GetComponent<BoxCollider2D>().enabled = true;
                package.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
            other.gameObject.SetActive(false);
        }
        /*if (other.gameObject.CompareTag("nest"))
        {
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }*/

        StartCoroutine(DestroySnowball());
    }

    IEnumerator DestroySnowball()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
