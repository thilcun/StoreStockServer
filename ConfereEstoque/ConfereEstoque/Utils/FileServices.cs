using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Utils
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileServices
    {
        public void GenerateFile(string directorypath, Ajuste ajuste)
        {
            string filepath = @"" + directorypath + "\\Ajuste" + ajuste.Codigo + ".txt";

            List<string> lines = new List<string>();
            foreach (ItemAjuste item in ajuste.Itens)
            {
                string line = string.Empty;
                line += item.Produto.CodigoProduto.ToString().PadRight(13, ' ');
                line += item.Quantidade.ToString().PadLeft(6, '0');
                string usr = item.Usuario + item.Endereco;
                line += usr.PadLeft(7, '0');
                string dt = item.Alterado.Day.ToString("00") + item.Alterado.Month.ToString("00") + item.Alterado.Year.ToString();
                line += dt;
                string hr = item.Alterado.Hour.ToString("00") + item.Alterado.Minute.ToString("00");
                line += hr;

                lines.Add(line);
            }
            try {
                System.IO.File.WriteAllLines(filepath, lines.ToArray());
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
