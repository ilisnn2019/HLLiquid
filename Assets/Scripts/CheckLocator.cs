using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocator : MonoBehaviour
{
    [SerializeField] 
    public Vector3 localTransform;
    [SerializeField] 
    public Vector3 worldTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        localTransform = transform.localRotation.eulerAngles;
        worldTransform = transform.rotation.eulerAngles;
    }
}
