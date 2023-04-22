using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class InteractObjGroup : MonoBehaviour
{
    public float speed = 2.0f;
    public float destroyTime = 40f;

    [SerializeField] private InteractObj[] interactables;
    // Start is called before the first frame update
    void Start()
    {
        //int randNum = Random.Range(0, interactables.Length);
        foreach(InteractObj interactable in interactables)
            interactable.isSelectable = true;
        Invoke("DestroyObj", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
