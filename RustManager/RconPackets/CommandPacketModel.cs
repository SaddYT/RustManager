namespace RustManager.RconPackets
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
