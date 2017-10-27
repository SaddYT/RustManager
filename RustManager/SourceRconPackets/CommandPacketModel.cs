namespace RustManager.SourceRconPackets
{
    public class CommandPacketModel : PacketModel
    {
        public CommandPacketModel(string command)
        {
            Type = PacketTypeModel.EXECCOMMAND;
            Body = command;
        }
    }
}
