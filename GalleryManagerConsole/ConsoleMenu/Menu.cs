using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GalleryManagerConsole.ConsoleMenu {
    public class Menu {
        public MenuItem ActiveItem { get; private set; }
        public int ActiveItemIndex { get; private set; }
        public int ActivePageIndex { get; private set; }
        public ConsoleColor HeaderColor { get; set; }
        public ConsoleColor ItemColor { get; set; }
        public ConsoleColor ActiveColor { get; set; }
        public string HeaderText { get; set; }
        public List<MenuItem> Items { get; set; }
        public bool Choice { get; set; }

        private readonly System.Timers.Timer updateTimer;
        private readonly Mutex mtx;
        private int Width { get; set; }
        private int Height { get; set; }

        public Menu() {
            Items = new List<MenuItem>();
            HeaderColor = ConsoleColor.White;
            ItemColor = ConsoleColor.Gray;
            ActiveColor = ConsoleColor.DarkRed;
            Choice = false;

            updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.Interval = 50;

            mtx = new Mutex();

            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }

        public void Show() {
            ActiveItem = Items[0];
            ActiveItemIndex = 0;
            Draw();
            Loop();
        }

        private void Loop() {
            bool exit = false;
            //updateTimer.Start();
            while (!exit) {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) {
                    if (ActiveItemIndex == 0) {
                        ActiveItemIndex = Items.Count - 1;
                        ActiveItem = Items[ActiveItemIndex];
                    }
                    else {
                        ActiveItemIndex--;
                        ActiveItem = Items[ActiveItemIndex];
                    }
                    Draw();
                }
                else if (key == ConsoleKey.DownArrow) {
                    if (ActiveItemIndex == Items.Count - 1) {
                        ActiveItemIndex = 0;
                        ActiveItem = Items[0];
                    }
                    else {
                        ActiveItemIndex++;
                        ActiveItem = Items[ActiveItemIndex];
                    }
                    Draw();
                }
                else if (key == ConsoleKey.Enter) {
                    if (ActiveItem.Input) {
                        ActiveItem.Text = string.Empty;
                        Draw();
                        Console.BackgroundColor = ActiveColor;
                        ActiveItem.Text = Console.ReadLine();
                        Console.SetCursorPosition(0, 0);
                    }
                    ActiveItem.OnItemSelected(new EventArgs());
                    if (Choice || ActiveItem.Exit)
                        exit = true;
                }
                else if (key == ConsoleKey.Escape)
                    exit = true;
            }
            //updateTimer.Stop();
            Console.Clear();
        }

        private void Draw() {
            mtx.WaitOne();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Press ESC to exit");
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;

            int x = (Width / 2) - (HeaderText.Length / 2);
            int y = Height / 7;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = HeaderColor;
            Console.Write(HeaderText);

            int optLength = Width / 3;
            for (int i = 0; i < Items.Count; i++) {
                // Add spaces for "optLength"
                int diff;
                if (Items[i].Input) {
                    diff = optLength - (Items[i].Title.Length + 1 + Items[i].Text.Length);
                    Items[i].DisplayTitle = Items[i].Title + " " + Items[i].Text;
                }
                else {
                    diff = optLength - Items[i].Title.Length;
                    Items[i].DisplayTitle = Items[i].Title;
                }


                for (int q = 0; q < diff; q++)
                    Items[i].DisplayTitle += " ";

                Console.ForegroundColor = ItemColor;
                if (Items[i] == ActiveItem)
                    Console.BackgroundColor = ActiveColor;
                else
                    Console.BackgroundColor = ConsoleColor.Black;

                x = (Width / 2) - (optLength / 2);
                y = Height / 4 + 3 + i;
                Console.SetCursorPosition(x, y);
                Console.Write(Items[i].DisplayTitle);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (ActiveItem.Input) {
                x = (Width / 2) - (optLength / 2) + ActiveItem.Title.Length + 1;
                y = Height / 4 + 3 + ActiveItemIndex;
                Console.SetCursorPosition(x, y);
            }
            else
                Console.SetCursorPosition(0, 0);
            mtx.ReleaseMutex();
        }

        private void UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) { 
                Update();
        }

        public void Update() =>
            Draw();

        public void ShowMessage(string msg) {
            mtx.WaitOne();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            int optLength = Width / 3;
            int boxWidth = optLength + 2;
            int boxHeight = msg.Length / optLength + 2;

            for (int i = 0; i < boxHeight - 2; i++) {
                int y = Height / 7;
                int x = (Width / 2) - ((optLength + 2) / 2);
                Console.SetCursorPosition(x, y);
                for (int q = 0; q < optLength + 2; q++)
                    Console.Write(" ");
            }
            mtx.ReleaseMutex();
        }
    }
}
