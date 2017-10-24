namespace RustManager.RCONPackets
{
    class RCONAuthPacket : RCONPacket
    {
        public RCONAuthPacket(string password)
        {
            this.ID = 0;
            this.Type = RCONPacketType.AUTH;
            this.Body = password;
        }
    }
}
