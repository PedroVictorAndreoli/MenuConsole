using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu("");

            // Adiciona itens com submenus e hints
            var item1 = new MenuItem { Rotulo = "Opcao 1", Hint = "Essa é a opção 1" };
            item1.SubItems.Add(new MenuItem { Rotulo = "SubItem 1.1", Hint = "Detalhe do SubItem 1.1" });
            item1.SubItems.Add(new MenuItem { Rotulo = "SubItem 1.2", Hint = "Detalhe do SubItem 1.2" });

            var item2 = new MenuItem { Rotulo = "Opcao 2", Hint = "Essa é a opção 2" };
            item2.SubItems.Add(new MenuItem { Rotulo = "SubItem 2.1", Hint = "Detalhe do SubItem 2.1" });
            item2.SubItems.Add(new MenuItem { Rotulo = "SubItem 2.2", Hint = "Detalhe do SubItem 2.2" });
            var item3 = new MenuItem { Rotulo = "Opcao 3", Hint = "Essa é a opção 3" };

            menu.Items.Add(item1);
            menu.Items.Add(item2);
            menu.Items.Add(item3);

            menu.Show();
            Console.ReadKey();
        }
    }
}
