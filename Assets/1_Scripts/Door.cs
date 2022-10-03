using UnityEngine;

namespace SceneObject
{
    public class Door : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GetComponent<Animator>().SetTrigger("Open");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                GetComponent<Animator>().SetTrigger("Close");
            }
        }
    }
}