namespace RustManager.RconPacketModels
{
    class AuthReponsePacketModel : PacketModel
    {
        public bool WasSuccessful() => Id != -1;
    }
}
