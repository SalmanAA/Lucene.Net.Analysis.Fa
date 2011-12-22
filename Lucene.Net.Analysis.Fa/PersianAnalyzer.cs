using System.Collections;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Version = Lucene.Net.Util.Version;

namespace Lucene.Net.Analysis.Fa
{
    public class PersianAnalyzer : Analyzer
    {
        private readonly Version _version;
        private readonly Hashtable _stoptable = new Hashtable();
        public static string DefaultStopwordFile = "PersianStopWords.txt";

        public PersianAnalyzer(Version version)
        {
            _version = version;
            var fileStream =
                System.Reflection.Assembly.GetAssembly(GetType()).GetManifestResourceStream("Lucene.Net.Analysis.Fa." +
                                                                                                 DefaultStopwordFile);
            if (fileStream != null)
                using (var reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var word = reader.ReadLine();
                        if (word != null) _stoptable.Add(word, word);
                    }
                }
        }

        public PersianAnalyzer(Version version, Hashtable stopWords)
        {
            _version = version;
            _stoptable = stopWords;
        }

        public override TokenStream TokenStream(string fieldname, TextReader reader)
        {
            TokenStream result = new StandardTokenizer(_version, reader);
            result = new LowerCaseFilter(result);
            result = new PersianNormalizationFilter(result);
            result = new StopFilter(StopFilter.GetEnablePositionIncrementsVersionDefault(_version), result, _stoptable);
            result = new PersianStemFilter(result);
            return result;
        }
    }

}
