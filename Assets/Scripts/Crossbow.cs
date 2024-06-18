using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] int maxAmmo = 3;

    [Header("Trackers")]
    [SerializeField] int currentAmmo = 3;

    [Header("Objects")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawn;
    void Start(){

    }

    public void Aim(Vector3 targetPosition){
        transform.rotation = Quaternion.LookRotation(Vector3.forward,targetPosition - transform.position);
    }
    public void Shoot(){
        if(currentAmmo < 1){
            return;
        }
        currentAmmo -= 1;
        GameObject newArrow = Instantiate(projectilePrefab,projectileSpawn.position,transform.rotation);
        Destroy(newArrow,20);
        newArrow.GetComponent<Projectile>().Launch(projectileSpeed);
        GetComponent<AudioSource>().Play();
    }

    public int GetCurrentAmmo(){
        return currentAmmo;
    }
    public int GetMaxAmmo(){
        return maxAmmo;
    }
}
