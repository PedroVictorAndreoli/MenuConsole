using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsole
{

    internal class MenuItem
    {
        public string Rotulo { get; set; }
        public int Col { get; set; }
        public int Lin { get; set; }
        public string Hint { get; set; }
        public List<MenuItem> SubItems { get; set; } = new List<MenuItem>();
        public ConsoleColor ItemForeground { get; set; }
        public ConsoleColor ItemBackground { get; set; }
        public ConsoleColor SelectorForeground { get; set; }
        public ConsoleColor SelectorBackground { get; set; }

        public MenuItem()
        {
            ItemForeground = ConsoleColor.White;
            ItemBackground = ConsoleColor.Black;
            SelectorBackground = ConsoleColor.Blue;
            SelectorForeground = ConsoleColor.Yellow;
        }

        public void Show()
        {
            Console.BackgroundColor = ItemBackground;
            Console.ForegroundColor = ItemForeground;
            Console.SetCursorPosition(Col, Lin);
            Console.Write(Rotulo);
        }

        public void ShowSelector()
        {
            Console.BackgroundColor = SelectorBackground;
            Console.ForegroundColor = SelectorForeground;
            Console.SetCursorPosition(Col, Lin);
            Console.Write(Rotulo);
        }
    }

    internal class Menu
    {
        public List<MenuItem> Items { get; set; }
        public int PosAtual { get; set; }
        public string Titulo { get; set; }
        public ConsoleColor TitleForeground { get; set; }
        public ConsoleColor TitleBackground { get; set; }

        public Menu(string titulo)
        {
            Items = new List<MenuItem>();
            TitleBackground = ConsoleColor.Blue;
            TitleForeground = ConsoleColor.Yellow;
            Titulo = titulo;
        }

        private void ShowHint(string hint)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write(hint);
        }

        private int Select()
        {
            Items[PosAtual].ShowSelector();
            ShowHint(Items[PosAtual].Hint);

            while (true)
            {
                var tecla = Console.ReadKey();
                Items[PosAtual].Show();
                switch (tecla.Key)
                {
                    case ConsoleKey.Enter:
                        ShowSubMenu(Items[PosAtual]);
                        return PosAtual;
                    case ConsoleKey.Escape:
                        return -1;
                    case ConsoleKey.RightArrow:
                        PosAtual = (PosAtual + 1) % Items.Count;
                        break;
                    case ConsoleKey.LeftArrow:
                        PosAtual = (PosAtual - 1 + Items.Count) % Items.Count;
                        break;
                }
                Items[PosAtual].ShowSelector();
                ShowHint(Items[PosAtual].Hint);
            }
        }

        public void Show()
        {
            int x = 0, y = 0;
            PosAtual = 0;
            Console.Clear();

            // Mostra título
            Console.BackgroundColor = TitleBackground;
            Console.ForegroundColor = TitleForeground;
            Console.SetCursorPosition((Console.WindowWidth - Titulo.Length) / 2, y);
            Console.WriteLine(Titulo);
            y += 2;

            // Mostra itens do menu na horizontal
            foreach (var item in Items)
            {
                item.Col = x;
                item.Lin = y;
                item.Show();
                x += item.Rotulo.Length + 3;  // Espaçamento entre itens
            }

            Console.CursorVisible = false;
            Select();
            Console.CursorVisible = true;
        }

        private void ShowSubMenu(MenuItem item)
        {
            Console.Clear();
            int x = (Console.WindowWidth - item.Rotulo.Length) / 2;
            int y = (Console.WindowHeight - item.SubItems.Count) / 2;

            // Mostra o título do submenu
            Console.BackgroundColor = TitleBackground;
            Console.ForegroundColor = TitleForeground;
            Console.SetCursorPosition(x, y - 2);
            Console.WriteLine(item.Rotulo);

            // Inicializa a seleção do submenu
            int posSubAtual = 0;
            foreach (var subItem in item.SubItems)
            {
                subItem.Col = x;
                subItem.Lin = y++;
                subItem.Show();
            }

            // Função de seleção do submenu
            while (true)
            {
                item.SubItems[posSubAtual].ShowSelector();
                ShowHint(item.SubItems[posSubAtual].Hint);
                var tecla = Console.ReadKey();
                item.SubItems[posSubAtual].Show();

                switch (tecla.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.SetCursorPosition(0, Console.WindowHeight - 2);
                        Console.WriteLine($"Selecionado: {item.SubItems[posSubAtual].Rotulo}");
                        Console.WriteLine("Pressione ESC para retornar...");
                        while (Console.ReadKey().Key != ConsoleKey.Escape) { }
                        Console.Clear();
                        Show();
                        return;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Show();
                        return;
                    case ConsoleKey.DownArrow:
                        posSubAtual = (posSubAtual + 1) % item.SubItems.Count;
                        break;
                    case ConsoleKey.UpArrow:
                        posSubAtual = (posSubAtual - 1 + item.SubItems.Count) % item.SubItems.Count;
                        break;
                }
            }
        }
    }
}
