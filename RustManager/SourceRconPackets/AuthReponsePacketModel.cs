namespace RustManager.SourceRconPackets
{
    public class AuthReponsePacketModel : PacketModel
    {
        public bool WasSuccessful() => Id != -1;
    }
}
