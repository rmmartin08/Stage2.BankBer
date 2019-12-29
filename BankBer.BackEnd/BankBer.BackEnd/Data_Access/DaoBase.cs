using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BankBer.BackEnd.Data_Access
{
    public class DaoBase
    {
        protected const string BankBerDbLocation = @"C:\BankBer\BankBer.db";

        public DaoBase()
        {
            var bankberDirectoryPath = Path.GetDirectoryName(BankBerDbLocation);
            if (!string.IsNullOrWhiteSpace(bankberDirectoryPath) && !Directory.Exists(bankberDirectoryPath))
            {
                Directory.CreateDirectory(bankberDirectoryPath);
            }
        }
    }
}