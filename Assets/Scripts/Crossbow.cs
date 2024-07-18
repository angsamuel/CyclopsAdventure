using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Crossbow : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] float reloadTime = 10;
    [SerializeField] float accuracy = 10;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float fireRate = 10;
    [SerializeField] int maxAmmo = 3;

    [Header("Trackers")]
    [SerializeField] int currentAmmo = 3;
    bool reloading = false;
    bool onCooldown = false;

    [Header("Objects")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Transform crossbowVisual;

    [Header("Generation")]
    [SerializeField] int seed = 0;
    public List<float> randomDist;
    float bestSpeed = 30f, worstSpeed = 5f;
    float bestReloadTime = .25f, worstReloadTime = 2f;
    float bestAccuracy = 0f, worstAccuracy = 45f;
    float bestRotation = 10f, worstRotation = 1f; 
    float bestFireRate = 10, worstFireRate = 1f;
    int bestMaxAmmo = 10, worstMaxAmmo = 1;



    void Start(){
        Generate();
    }

    public void Aim(Vector3 targetPosition){
        //transform.rotation = Quaternion.LookRotation(Vector3.forward,targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Vector3.forward,targetPosition - transform.position), rotationSpeed*Time.fixedDeltaTime);
    }
    public void Shoot(){
        if(onCooldown){
            return;
        }
        if(reloading){
            return;
        }
        if(currentAmmo < 1){
            return;
        }
        onCooldown = true;
        StartCoroutine(ShotCooldownRoutine());

        currentAmmo -= 1;
        GameObject newArrow = Instantiate(projectilePrefab,projectileSpawn.position,transform.rotation);
        Destroy(newArrow,20);
        newArrow.transform.localEulerAngles += new Vector3(0,0,Random.Range(-accuracy,accuracy)); //procgen accuracy
        newArrow.GetComponent<Projectile>().Launch(projectileSpeed);
        GetComponent<AudioSource>().Play();
    }

    IEnumerator ShotCooldownRoutine(){
        yield return new WaitForSeconds(1/fireRate);
        onCooldown = false;
    }

    public int GetCurrentAmmo(){
        return currentAmmo;
    }
    public int GetMaxAmmo(){
        return maxAmmo;
    }



    
    //stats



    

    //new crossbow behavior
    public void Reload(){
        if(reloading){
            return;
        }
        reloading = true;
        StartCoroutine(ReloadRoutine());
        IEnumerator ReloadRoutine(){
            float t = 0;
            while(t<reloadTime){
                t+=Time.deltaTime;
                crossbowVisual.transform.localEulerAngles = new Vector3(0,(t/reloadTime)*180,0);
                yield return null;
            }
            crossbowVisual.transform.localEulerAngles = new Vector3(0,0,0);
            currentAmmo = maxAmmo;
            reloading = false;
        }
    }

    public void Generate(){

        randomDist = Cylib.GetDistribution(seed,6);
        projectileSpeed = Mathf.Lerp(worstSpeed, bestSpeed,randomDist[0]); //done
        reloadTime = Mathf.Lerp(worstReloadTime, bestReloadTime, randomDist[1]); //done
        accuracy = Mathf.Lerp(worstAccuracy, bestAccuracy, randomDist[2]); //done
        rotationSpeed = Mathf.Lerp(worstRotation, bestRotation, randomDist[3]); //done
        fireRate = Mathf.Lerp(worstFireRate, bestFireRate, randomDist[4]); 
        maxAmmo = (int)Mathf.Lerp(worstMaxAmmo, bestMaxAmmo+1, randomDist[5]); //done




        // Random.InitState(8);
        // projectileSpeed = Random.Range(worstSpeed,bestSpeed); 
        // reloadTime = Random.Range(worstReloadTime, bestReloadTime); 
        // accuracy = Random.Range(worstAccuracy, bestAccuracy);
        // rotationSpeed = Random.Range(worstRotation,bestRotation);
        // fireRate = Random.Range(worstFireRate, bestFireRate);
        // maxAmmo = Random.Range(worstMaxAmmo, bestMaxAmmo);
        // currentAmmo = maxAmmo;
    }
}

[CustomEditor(typeof(Crossbow))]
public class CrossbowEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Generate"))
		{
			var creator = (Crossbow)target;
			creator.Generate();
		}


		EditorGUILayout.EndHorizontal();
	}
}
