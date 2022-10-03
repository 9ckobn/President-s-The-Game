using Photon.Pun;
using UnityEngine;
using SceneObjects;
using UI;
using Character;

namespace Core
{
    [CreateAssetMenu(fileName = "HubController", menuName = "Controllers/HubController")]
    public class HubController : BaseController
    {
        private PersonController personController = null;

        public override void OnStart()
        {
            BoxController.GetController<LogController>().Log("Start Hub Manager");
            personController = PhotonNetwork.Instantiate(NamesData.PERSON_PREFAB_NAME, ObjectsOnScene.Instance.GetSpawnPerson.transform.position, Quaternion.identity).GetComponent<PersonController>();
            personController.Initialize(BoxController.GetController<DataPersonController>().GetNickUser);

            ObjectsOnScene.Instance.GetVirtualCamera.Follow = personController.GetCameraRoot.transform;

            UIManager.Instance.HideWindow<ConnectLobbyWindow>();
            UIManager.Instance.HideWindow<BackgroundWindow>();
        }

        public void NewNickName(string name)
        {
            personController.SetNewName = name;
        }
    }
}
