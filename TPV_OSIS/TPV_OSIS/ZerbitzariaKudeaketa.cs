namespace TPV_OSIS
{
    public static class ZerbitzariaKudeaketa
    {
        private static Zerbitzaria zerbitzaria;
        private static readonly object _lock = new object();

        public static void ZerbitzariaPiztuta(int portua = 3306)
        {
            lock (_lock)
            {
                if (zerbitzaria == null)
                {
                    zerbitzaria = new Zerbitzaria(portua);
                    zerbitzaria.Hasi();
                }
            }
        }

        public static void Gelditu()
        {
            lock (_lock)
            {
                if (zerbitzaria != null)
                {
                    zerbitzaria.Gelditu();
                    zerbitzaria= null;
                }
            }
        }
    }
}
