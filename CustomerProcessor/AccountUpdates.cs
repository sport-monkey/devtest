using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FileParser;

namespace CustomerProcessor
{
    public class AccountUpdates
    {
        public Collection<IAccountDetails> Accounts { get; private set; }

        public AccountUpdates(string path)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(path);
            ILoader<BasicAccountDetails> loader = new CompressedLoader<BasicAccountDetails>();

            IList<IAccountDetails> details = files.Select(f => (IAccountDetails)loader.FromFile(f)).ToList();
            Accounts = new Collection<IAccountDetails>(details);
        }

        public decimal TotalCredit
        {
            get { return Accounts.Sum(a => a.Creditors.Sum(c => c.Value)); }
        }
    }
}
