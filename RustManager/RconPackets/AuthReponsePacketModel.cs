namespace RustManager.RconPackets
{
    public class AuthReponsePacketModel : PacketModel
    {
        public bool WasSuccessful() => Id != -1;
    }
}
