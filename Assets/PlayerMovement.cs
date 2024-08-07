using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Transform  finishPoint;
    private Renderer colortochange;
    public GameObject[] blastEffects;
    public int waitForBlast = 3;


    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;

        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;

        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;

        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;

        }
        if (movement.magnitude > 1) {
            movement.Normalize();            
        }

        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime;
        transform.position = newPosition;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill"))
        {
            StartCoroutine(CubeMover(other.gameObject));
        }
    }

    IEnumerator CubeMover(GameObject gameObject)
    {
        float startingPosition = 0f;
        float endPosition = 5f;

        while (startingPosition < endPosition)
        {
            startingPosition += Time.deltaTime;
            float t = startingPosition / endPosition;
            float c = t * endPosition;

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finishPoint.transform.position, t);
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, c);
            yield return null;
        }

        BlastEffect();
        Destroy(gameObject);
    }

    private void BlastEffect()
    {
        for(int i = 0; i < blastEffects.Length; i++)
        {
            blastEffects[i].SetActive(true);
            return;
        }
    }
}