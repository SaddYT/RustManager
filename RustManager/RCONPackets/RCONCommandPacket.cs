namespace RustManager.RCONPackets
{
    class RCONCommandPacket : RCONPacket
    {
        public RCONCommandPacket(string command)
        {
            this.Type = RCONPacketType.EXECCOMMAND;
            this.Body = command;
        }
    }
}
