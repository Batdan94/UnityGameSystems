using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistController : MonoBehaviour
{
    public GameObject circle;
    public bool game = true;
    public AudioSource fistSource;
    public AudioClip fistSound;

    // Use this for initialization
    void Start()
    {
        fistSource = GetComponent<AudioSource>();

        StartCoroutine(raisingFist(gameObject));
        StartCoroutine(AudioFadeOut.FadeOut(fistSource, 1f)); 
        if (game)
        {
            foreach (var boid in GameManager.Instance.BoidsManager.robotZombies)
            {
                if (Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z), circle.transform.position) < circle.GetComponent<Circle>().xradius)
                {
                    if (boid.active)
                    {
                        boid.GetComponent<BoidStats>().StartCoroutine(boid.GetComponent<BoidStats>().squash(boid));
                    }
                    //boid.gameObject.SetActive(false);
                }
                else if (Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z), circle.transform.position) > circle.GetComponent<Circle>().xradius &&
                        Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z), circle.transform.position) < (circle.GetComponent<Circle>().xradius * 2))
                {
                    //Debug.Log("Is within Blast radius");
                    boid.GetComponent<Rigidbody>().AddExplosionForce(1000.0f, circle.transform.position, circle.GetComponent<Circle>().xradius * 2);
                    boid.GetComponent<Rigidbody>().AddForce(new Vector3(0, 3000, 0));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator raisingFist(GameObject fistinst)
    {
        while (fistinst.transform.position.y > 0.0f)
        {
            if ((fistinst.transform.position + Vector3.down * Time.deltaTime * 200).y < 0.0f)
            {
                fistinst.transform.position = new Vector3(fistinst.transform.position.x, 0.0f, fistinst.transform.position.z);
            }
            else
            {
                fistinst.transform.position = (fistinst.transform.position + Vector3.down * Time.deltaTime * 200);
            }
            yield return new WaitForFixedUpdate();
        }
        Timer timer = new Timer(2.0f);
        while (!timer.Trigger())
        {
            fistinst.transform.position += Vector3.up * Time.deltaTime * 5;
            yield return new WaitForFixedUpdate();
        }
        Destroy(fistinst);
        yield return null;
    }
}
