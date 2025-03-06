// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("oPv6q1/knxJeKrsyZvI/fvwDyuk+Fdq3hRRBCvtIKetHwTZSx25dcZWP0x5pr9aec047MqQdAml8Yk5dCHEeWoKdN5swjrH7qwKuKyoiGWDXZebF1+rh7s1hr2EQ6ubm5uLn5OIn4Ke0zW1eWZcy5mtOEOlk1u6NA4BSG635khrgPKHKVqe1fSPcuY3WrlBs7RXCSI5vgO4s1cZZg91D9ncEyRqF6mmqlM5ghhB9YtNXqRBfJugA/B0te9/6Zy0qQIZJ8qZIaO3M3vHNbRmD2+osqgeaCEFQmhnPvWXm6OfXZebt5WXm5uchmgS7JgAk6zpQq4BFsCsCY5O5eDOCgRgZZDaWwPtvbvbPvtpK+5V+o9xFFO6AW8QFNowc7u1TeuXk5ufm");
        private static int[] order = new int[] { 8,2,4,6,8,9,6,9,12,10,10,12,13,13,14 };
        private static int key = 231;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
