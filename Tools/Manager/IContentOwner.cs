using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zadontseva_01.Tools.Managers
{
    interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
