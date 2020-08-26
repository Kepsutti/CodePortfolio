using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 *          INFO
 *          
 *     This SpiralAround function was used in an adventure game where the played character collects fireflies from a map.
 *     When a firefly is collected, it performs an action of circling around the player in a spiral until it reaches
 *     the position of the player, at which point it would disappear and launch a particle effect.
 *     
*/



public class NPC : MonoBehaviour
{
    private Character player;
    public TutoLvl TutorialCutsceneScript;
    public GameObject FiaChild, burstParticleSystem;

    public IEnumerator SpiralAround()
    {
        //The collected firefly circles the player in a spiral until it's close enough to the player's position+
        Vector3 temp;
        temp = player.transform.position;
        temp.z++;

        //The firefly is appointed as a child of the player character so that player's potential movement won't disturb that of the firefly's
        GameObject parentHolder = transform.parent.gameObject;
        transform.parent = player.transform;

        int spiralSpeed = 30;
        float distance = (transform.position - player.transform.position).magnitude / 150;
        float halfDistance = (transform.position - player.transform.position).magnitude / 2;
        Collider col = null;
        if (FiaChild.GetComponentInChildren<Collider>() != null)
        {
            col = FiaChild.GetComponentInChildren<Collider>();
            col.enabled = false;
        }

        //Spiraling motion
        while (transform.position.x != player.transform.position.x && transform.position.z != player.transform.position.z)
        {
            yield return new WaitForEndOfFrame();
            temp = player.transform.position;
            temp.y += 0.6f;

            transform.RotateAround(temp, Vector3.up, spiralSpeed * Time.deltaTime);
            spiralSpeed += 5;

            temp = transform.position;
            temp.y = player.transform.position.y;

            if ((transform.position - player.transform.position).magnitude < halfDistance && transform.localScale.x > 0)
            {
                Vector3 tmp = new Vector3(transform.localScale.x - 0.001f, transform.localScale.x - 0.001f, transform.localScale.x - 0.001f);
                transform.localScale -= new Vector3(0.0025f, 0.0025f, 0.0025f);
                GetComponentInChildren<Light>().intensity -= 0.05f;
            }

            if ((temp - player.transform.position).magnitude > 0.2f)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, distance);
            else
                transform.position = player.transform.position;
            yield return null;
        }

        //Particle effect
        burstParticleSystem.SetActive(true);
        
        FiaChild.SetActive(false);
        this.GetComponent<Collider>().enabled = false;
        //Reassigning the original parent of the firefly gameobject
        transform.parent = parentHolder.transform;
    }
}
