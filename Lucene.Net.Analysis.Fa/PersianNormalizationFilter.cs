using Lucene.Net.Analysis.Tokenattributes;

namespace Lucene.Net.Analysis.Fa
{
    public sealed class PersianNormalizationFilter : TokenFilter
    {
        private readonly PersianNormalizer _normalizer;
        private readonly TermAttribute _termAtt;

        public PersianNormalizationFilter(TokenStream input)
            : base(input)
        {
            _normalizer = new PersianNormalizer();
            _termAtt = (TermAttribute)AddAttribute<TermAttribute>();
        }

        public override bool IncrementToken()
        {
            if (input.IncrementToken())
            {
                int newLen = _normalizer.Normalize(_termAtt.TermBuffer(), _termAtt.TermLength());
                _termAtt.SetTermLength(newLen);
                return true;
            }
            return false;
        }
    }
}
