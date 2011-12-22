using Lucene.Net.Analysis.Tokenattributes;

namespace Lucene.Net.Analysis.Fa
{
    public sealed class PersianStemFilter : TokenFilter
    {
        private readonly PersianStemmer _stemmer;
        private readonly TermAttribute _termAttr;

        public PersianStemFilter(TokenStream input) : base(input)
        {
            _stemmer = new PersianStemmer();
            _termAttr = (TermAttribute)AddAttribute(typeof(TermAttribute));
        }

        public override bool IncrementToken()
        {
            if (input.IncrementToken())
            {
                var newLength = _stemmer.Stem(_termAttr.TermBuffer(), _termAttr.TermLength());
                _termAttr.SetTermLength(newLength);
                return true;
            }

            return false;
        }
    }
}
