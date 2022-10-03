using Photon.Pun;
using UI;

namespace Core
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        public void Connect()
        {
            UIManager.Instance.ShowWindow<ConnectLobbyWindow>();

            PhotonNetwork.NickName = "123";
            BoxController.GetController<LogController>().Log("Connect to photon...");

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            BoxController.GetController<LogController>().Log("On connect to master");

            if(PhotonNetwork.CountOfRooms > 0)
            {
                JoinRoom();
            }
            else
            {
                CreateRoom();
            }
        }

        public void CreateRoom()
        {
            BoxController.GetController<LogController>().Log("Create room...");

            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 20 });
        }

        public void JoinRoom()
        {
            BoxController.GetController<LogController>().Log("Join to room...");

            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            BoxController.GetController<LogController>().Log("On join to room...");

            PhotonNetwork.LoadLevel(1);
        }
    }
}