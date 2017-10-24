namespace RustManager.RconPacketModels
{
    class CommandPacketModel : PacketModel
    {
        public CommandPacketModel(string command)
        {
            Type = PacketTypeModel.EXECCOMMAND;
            Body = command;
        }
    }
}
