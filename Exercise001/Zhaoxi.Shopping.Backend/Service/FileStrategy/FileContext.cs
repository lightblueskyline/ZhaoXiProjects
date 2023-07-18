﻿using Microsoft.AspNetCore.Http;

namespace Service.FileStrategy
{
    /// <summary>
    /// 通过此上下文，调用具体的策略
    /// </summary>
    public class FileContext
    {
        private Strategy _strategy;
        private List<IFormFile> _formFiles;

        public FileContext(Strategy strategy, List<IFormFile> formFiles)
        {
            _strategy = strategy;
            _formFiles = formFiles;
        }

        public async Task<string> ContextInterface()
        {
            return await _strategy.Upload(_formFiles);
        }
    }
}
