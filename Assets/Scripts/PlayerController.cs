using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    
    public float speed = 5.0f;
    public float powerUpStrength = 15.0f;

    //private bool isBraking;
    public bool hasPowerup = false;

    // Getting components initializing values and finding coordinates 
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        //isBreaking = false;
    }

    // player movement
    void Update()
    {// Player movement "GetAxisRaw" makes movement more sharp
        float upDownInput = Input.GetAxisRaw("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * upDownInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        /*if (Input.GetKey(KeyCode.Space))
        {
            isBreaking = true;
          
        }
        else
        {
            isBreaking = false;;
        }*/
    }
    // Collision events (powerup's and routines)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    // "CO routine" is a timer that will countdown before setting "hasPowerup" to false
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    //Powerup collision calculations
    private void OnCollisionEnter(Collision collision)
    {// if has powerup and hits enemy
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {// take into acount enemy direction
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // Enemy component we got (Current enemy position) - our position = where to go
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            
            //add force to the enemy using calculation above for trajectory and launch him backwards 
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            //Debug test for appropriate interactions between components 
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set to " + hasPowerup);
        }
    }
}
