using Photon.Pun;
using UI;

namespace Core
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        public void Connect()
        {
            UIManager.ShowWindow<ConnectLobbyWindow>();

            PhotonNetwork.NickName = "123";
            LogManager.Log("Connect to photon...");

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            LogManager.Log("On connect to master");

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
            LogManager.Log("Create room...");

            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 20 });
        }

        public void JoinRoom()
        {
            LogManager.Log("Join to room...");

            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            LogManager.Log("On join to room...");

            PhotonNetwork.LoadLevel(1);
        }
    }
}