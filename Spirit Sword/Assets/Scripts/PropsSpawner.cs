using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> propsList = new List<GameObject>();
    [SerializeField] List<GameObject> greenList = new List<GameObject>();
    public static List<GameObject> areaList = new List<GameObject>();
    public  List<GameObject> propList = new List<GameObject>();
    [SerializeField] GameObject area;
    int spawnCount = 70;
    int flowers = 300;
    GameObject player;
    Transform playerTransform;
    float distanceX, distanceZ;
    bool canCreate = true;
    // Start is called before the first frame update
    void Start()
    {
        areaList.Add(gameObject);
        player = GameObject.Find("MainCharacter");
        playerTransform = player.transform;

        foreach (var item in propList)
        {
            Destroy(item);
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnArea = new Vector3(Random.Range(transform.position.x - 25f, transform.position.x + 25f), 0.5f, Random.Range(transform.position.z - 25f, transform.position.z + 25f));
            int index = Random.Range(0, propsList.Count);

            Quaternion newRotation = Quaternion.Euler(propsList[index].transform.rotation.x, Random.Range(0, 360), propsList[index].transform.rotation.z);
            GameObject props = Instantiate(propsList[index], spawnArea, newRotation);
            props.transform.parent = gameObject.transform;
            propList.Add(props);
        }
        for (int i = 0; i < flowers; i++)
        {
            Vector3 spawnArea = new Vector3(Random.Range(transform.position.x - 25f, transform.position.x + 25f), 0.5f, Random.Range(transform.position.z - 25f, transform.position.z + 25f));
            int index = Random.Range(0, greenList.Count);

            Quaternion newRotation = Quaternion.Euler(greenList[index].transform.rotation.x, Random.Range(0, 360), greenList[index].transform.rotation.z);
            GameObject props = Instantiate(greenList[index], spawnArea, newRotation);
            props.transform.parent = gameObject.transform;
            propList.Add(props);
        }
    }

    void Check(Vector3 test)
    {
        for (int i = 0; i < areaList.Count; i++)
        {
            if(areaList[i].transform.position.x == test.x && areaList[i].transform.position.z == test.z)
            {
                Debug.Log("Zaten var");
                canCreate = false;
            }
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        distanceX = Mathf.Abs(transform.position.x - playerTransform.position.x);
        distanceZ = Mathf.Abs(transform.position.z - playerTransform.position.z);
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if(distanceX > 75)
        {
            if (transform.position.x - playerTransform.position.x > 75)
            {
                Vector3 newPosition = new Vector3(transform.position.x - 150, transform.position.y, transform.position.z);
                if (canCreate)
                {
                    GameObject newArea = Instantiate(area, newPosition, area.transform.rotation);
                    newArea.name = "OtoGround";
                    canCreate = false;
                    areaList.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            else if (transform.position.x - playerTransform.position.x <= 75)
            {
                Vector3 newPosition = new Vector3(transform.position.x + 150, transform.position.y, transform.position.z);
                if (canCreate)
                {
                    
                    GameObject newArea = Instantiate(area, newPosition, area.transform.rotation);
                    newArea.name = "OtoGround";
                    canCreate = false;
                    areaList.Remove(gameObject);
                    Destroy(gameObject);
                }
            }

        }
        if(distanceZ > 75)
        {

            if (transform.position.z - playerTransform.position.z > 75 && canCreate)
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 150);
                if (canCreate)
                {
                    
                    GameObject newArea = Instantiate(area, newPosition, area.transform.rotation);
                    newArea.name = "OtoGround";
                    canCreate = false;
                    areaList.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            else if (transform.position.z - playerTransform.position.z <= 75 && canCreate)
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 150);
                if (canCreate)
                {
                    
                    GameObject newArea = Instantiate(area, newPosition, area.transform.rotation);
                    newArea.name = "OtoGround";
                    canCreate = false;
                    areaList.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
        }

    }
}
