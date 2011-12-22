using System;
using System.Linq;

namespace Lucene.Net.Analysis.Fa
{
    class PersianStemmer
    {
        private const char Alef = 'ا';
        private const char Heh = 'ه';
        private const char Teh = 'ت';
        private const char Nun = 'ن';
        private const char YehPersian = 'ي';
        private const char Gaf = 'گ';

        public static char[][] Suffixes = {
                                              new [] {Gaf,Alef,Nun}, // gan => mordegan
                                              new [] { Heh , Alef}, // ha => ensanha
                                              new [] { Alef , Teh}, // at => adavat
                                              new [] {Alef,Nun}, // an => dustan
                                              new [] {YehPersian, Nun}, // in => moslemin
                                              new [] {Gaf, YehPersian}, // gi => zendegi
                                              new [] {Heh}
                                          };

        public int Stem(char[] input, int len)
        {
            return StemSuffix(input, len);
        }

        private int StemSuffix(char[] input, int len)
        {
            foreach (char[] t in Suffixes)
            {
                if(EndsWith(input, len, t))
                {
                    len = DeleteN(input, len - t.Length, len, t.Length);
                }
            }
            return len;
        }

        private bool EndsWith(char[] s, int len, char[] suffix)
        {
            if (len < suffix.Length + 2)
            { // all suffixes require at least 2 characters after stemming
                return false;
            }
            return !suffix.Where((t, i) => s[len - suffix.Length + i] != t).Any();
        }


        protected int DeleteN(char[] s, int pos, int len, int nChars)
        {
            for (int i = 0; i < nChars; i++)
                len = Delete(s, pos, len);
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
