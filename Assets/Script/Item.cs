using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{

    private void Start()
    {
        
    }

    //�v���C���[�̎��E�͈�
    private void OnTriggerStay(Collider other)
    {
        //���E�͈͓�
        if (other.gameObject.name == "ItemSerchArea")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
