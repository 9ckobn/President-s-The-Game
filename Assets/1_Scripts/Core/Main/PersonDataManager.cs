namespace Core
{
    public class PersonDataManager : Singleton<PersonDataManager>
    {
        private string nickUser;
        private string keyUser;

        public string NickUser { get => nickUser; set => nickUser = value; }
        public string KeyUser { get => keyUser; set => nickUser = keyUser; }
    }
}