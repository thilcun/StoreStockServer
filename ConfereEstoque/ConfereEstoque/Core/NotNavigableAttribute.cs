using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    class NotNavigableAttribute : Attribute
    {
    }
}
