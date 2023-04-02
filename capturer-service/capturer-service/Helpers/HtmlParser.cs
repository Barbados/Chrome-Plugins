using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace capturerservice.Helpers
{
    public class HtmlParser
    {
        private readonly string _text;
        private readonly Regex _wordPattern = new Regex(@"<h1 class=""headword"".*?>(.*\w)<\/h1>", RegexOptions.Compiled);
        private readonly Regex _definitionsPattern = new Regex(@"<span class=""def"".*?>(.*?)<\/span>", RegexOptions.Compiled);

        public HtmlParser(string content)
        {
            _text = content;
        }

        public string GetWord()
        {
            return _wordPattern.Match(_text).Groups[1].Value;
        }

        public List<string> GetDefinitions()
        {
            var result = new List<string>();

            foreach (Match match in _definitionsPattern.Matches(_text))
            {
                result.Add(match.Groups[1].Value);
            }

            return result;
        }
    }
}
