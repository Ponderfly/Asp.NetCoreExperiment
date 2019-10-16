﻿

using Microsoft.Extensions.Logging;
using System;

namespace DIChainOfResponsibility
{
  
    public class ThirdTransfer : ParentTransfer
    {
        readonly ILogger<ThirdTransfer> _logger;
        public ThirdTransfer(ILogger<ThirdTransfer> logger, EndTransfer serviceAccessor)
        {
            this.Next(serviceAccessor);
            _logger = logger;
        }
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="transferParmeter">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter transferParmeter)
        {
            var result = SelfTransfer(transferParmeter);
            return _parentTransfer.Transfer(transferParmeter) && result;
        }
        bool SelfTransfer(TransferParmeter transferParmeter)
        {
            _logger.LogInformation("-------------------------------------------ThirdTransfer");
            return true;
        }

    }
}
