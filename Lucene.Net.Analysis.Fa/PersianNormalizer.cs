using System;

namespace Lucene.Net.Analysis.Fa
{
    class PersianNormalizer
    {
        private const char Fathe = 'َ';
        private const char Zamme = 'ُ';
        private const char Kasre = 'ِ';
        private const char TanvinFathe = 'ً';
        private const char TanvinZamme = 'ٌ';
        private const char TanvinKasre = 'ٍ';
        private const char Tashdid = 'ّ';
        private const char Sokun = 'ْ';
        private const char HamzeJoda = 'ء';
        private const char KhateTire = 'ـ';
        private const char YehArabic = 'ی';
        private const char YehPersian = 'ي';
        private const char KafArabic = 'ک';
        private const char KafPersian = 'ك';

        public int Normalize(char[] input, int len)
        {
            for (var i = 0; i < len; i++)
            {
                if (input[i] == YehArabic)
                    input[i] = YehPersian;
                else if (input[i] == KafArabic)
                    input[i] = KafPersian;
                if (input[i] == HamzeJoda || input[i] == Fathe || input[i] == Zamme ||
                    input[i] == Kasre || input[i] == TanvinFathe || input[i] == TanvinZamme ||
                    input[i] == TanvinKasre || input[i] == Sokun || input[i] == KhateTire || input[i] == Tashdid)
                {
                    len = Delete(input, i, len);
                    i--;
                }
            }
            return len;
        }

        protected int Delete(char[] s, int pos, int len)
        {
            if (pos < len)
                Array.Copy(s, pos + 1, s, pos, len - pos - 1);

            return len - 1;
        }
    }
}
