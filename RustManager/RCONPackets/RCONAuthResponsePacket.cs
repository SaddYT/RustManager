namespace RustManager.RCONPackets
{
    class RCONAuthResponsePacket : RCONPacket
    {
        public bool WasSuccessful() => this.ID != -1;
    }
}
