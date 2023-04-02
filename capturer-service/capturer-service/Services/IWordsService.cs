using System;
using System.Collections.Generic;
using capturerservice.Model;

namespace capturerservice.Services
{
    public interface IWordsService
    {
        public WordElement Get(string word);

        public WordElement Create(string name);

        public List<WordElement> Get();
    }
}
