using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    public GameObject afterImagePrefab;

    private Queue<GameObject> availableImages = new Queue<GameObject>();

    public static AfterImagePool Instance { get; private set; }

    private void  Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool(){
        for (int i = 0; i < 10; i++){
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance){
        instance.SetActive(false);
        availableImages.Enqueue(instance);
    }

    public GameObject GetFromPool(){
        if(availableImages.Count == 0){
            GrowPool();
        }

        var instance = availableImages.Dequeue();
        return instance;
    }
}
