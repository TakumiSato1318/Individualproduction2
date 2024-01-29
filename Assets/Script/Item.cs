using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{

    private void Start()
    {
        
    }

    //ƒvƒŒƒCƒ„[‚Ì‹ŠE”ÍˆÍ
    private void OnTriggerStay(Collider other)
    {
        //‹ŠE”ÍˆÍ“à
        if (other.gameObject.name == "ItemSerchArea")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
