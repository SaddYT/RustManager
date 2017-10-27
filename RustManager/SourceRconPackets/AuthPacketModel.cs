namespace RustManager.SourceRconPackets
{
    public class AuthPacketModel : PacketModel
    {
        public AuthPacketModel(string password)
        {
            Id = 0;
            Type = PacketTypeModel.AUTH;
            Body = password;
        }
    }
}
