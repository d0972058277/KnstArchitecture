namespace System
{
    public static class GuidExtension
    {
        private readonly static long BaseDateTicks = new DateTime(1900, 1, 1).Ticks;

        public static Guid Sequential(this Guid guid)
        {
            DateTime now = DateTime.UtcNow;

            int days = new TimeSpan(now.Ticks - BaseDateTicks).Days;
            TimeSpan msecs = now.TimeOfDay;

            byte[] daysArray = BitConverter.GetBytes(days);
            byte[] msecsArray = BitConverter.GetBytes((int)msecs.TotalMilliseconds);

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            byte[] guidArray = guid.ToByteArray();

            Buffer.BlockCopy(daysArray, daysArray.Length - 2, guidArray, 0, 2);
            Buffer.BlockCopy(msecsArray, 0, guidArray, 2, 4);

            return new Guid(guidArray);
        }

    }
}
