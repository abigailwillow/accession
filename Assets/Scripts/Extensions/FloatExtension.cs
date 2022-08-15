namespace Accession.Extensions {
    public static class FloatExtension {
        public static float ClampLoop(this float value, float min = 0, float max = 1) {
            if (value < min) return max - (min - value);
            if (value > max) return min + (value - max);
            return value;
        }
    }
}