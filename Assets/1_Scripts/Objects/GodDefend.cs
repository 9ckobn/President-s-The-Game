using Buildings;
using UnityEngine;

namespace SceneObjects
{
    public class GodDefend : MonoBehaviour
    {
        private Building building;
        public Building SetBuilding { set => building = value; }

        private void OnMouseOver()
        {
            building.OnMouseOver();
        }

        private void OnMouseExit()
        {
            building.OnMouseExit();
        }

        private void OnMouseDown()
        {
            building.OnMouseDown();
        }
    }
}