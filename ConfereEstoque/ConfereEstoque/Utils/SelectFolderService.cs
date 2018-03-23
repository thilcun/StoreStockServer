using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfereEstoque.Utils
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SelectFolderService
    {
        public string GetFolderSelected()
        {
            var dlg = new FolderBrowserDialog();
            DialogResult result = dlg.ShowDialog();
            return dlg.SelectedPath;
        }
    }
}
