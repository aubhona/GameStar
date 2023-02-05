using System;

namespace GameStar
{
    internal static class Program
    {
        // Класс, опысывающий персонажей игры.
        private class Npc
        {
            // X координата персонажа игры.
            public int X;
            // Y координата персонажа игры.
            public int Y;
            // Цвет, которым рисуется персонаж.
            private readonly ConsoleColor _color;
            // Список следов врага.
            public static readonly List<(int, int)> Trace = new List<(int, int)>();
            // Тип персонажа.
            private readonly string _type;
            // Количество, собарнных игроком монет.
            public static int AmountCoinPlayer = 0;
            // Количество, собранных врагом монет.
            public static int AmountCoinEnemy = 0;
            // X координата монеты.
            public static int CoinX = 0;
            // Y координата монеты.
            public static int CoinY = 0;
            // Генератор случайных чисел.
            public static readonly Random Rnd = new Random();
            // Перемена, описывающая, собрана ли монета.
            private static bool _coinCollected = false;
            // Список созданных персонажей.
            public static readonly List<Npc> NpcList = new List<Npc>();
            // Настройки окна.
            public static (int, int) WindowSetting = (Console.WindowWidth, Console.WindowHeight);

            public Npc(int inpX, int inpY, string inpType, ConsoleColor inpColor)
            {
                X = inpX;
                Y = inpY;
                _type = inpType;
                _color = inpColor;
                
                if (inpType == "$")
                {
                    CoinX = inpX;
                    CoinY = inpY;
                }
                
                NpcList.Add(this);
                 
                if (inpType == "%")
                {
                    Trace.Add((inpX, inpY));
                }
                
                Draw();
            }
            
            // Метод, рисующий персонажей.
            private void Draw()
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"Player: {AmountCoinPlayer}");
                Console.WriteLine($"Enemy: {AmountCoinEnemy}");
                Console.ForegroundColor = _color;
                Console.SetCursorPosition(X, Y);
                Console.Write(_type);
            }

            // Метод, форматирующий координаты в корректный вид.
            public void ChangeCoord()
            {
                X = (int)Math.Round(X * (Console.WindowWidth / (double)WindowSetting.Item1), 
                    MidpointRounding.ToEven);
                Y = (int)Math.Round(Y * (Console.WindowHeight / (double)WindowSetting.Item2), 
                    MidpointRounding.ToEven);
                
                CheckCoord();

                if (_type == "$")
                {
                    CoinX = X;
                    CoinY = Y;
                }
                
                NpcList[NpcList.IndexOf(this)] = this;
                
                Draw();
            }
            
            // Метод, форматирующий координаты в корректный вид.
            private void CheckCoord()
            {
                if (X >= 3 * Console.WindowWidth / 4)
                {
                    X = Console.WindowWidth / 4 + 1;
                }
                else if (X <= Console.WindowWidth / 4)
                {
                    X = 3 * Console.WindowWidth / 4 - 1;
                }
                if (Y >= 3 * Console.WindowHeight / 4 - 1)
                {
                    Y = Console.WindowHeight / 4;
                }
                else if (Y <= Console.WindowHeight / 4 - 1)
                {
                    Y = 3 * Console.WindowHeight / 4 - 2;
                }
            }
            
            public static void CheckCoord(ref int x, ref int y)
            {
                if (x >= 3 * Console.WindowWidth / 4)
                {
                    x = Console.WindowWidth / 4 + 1;
                }
                else if (x <= Console.WindowWidth / 4)
                {
                    x = 3 * Console.WindowWidth / 4 - 1;
                }
                if (y >= 3 * Console.WindowHeight / 4 - 1)
                {
                    y = Console.WindowHeight / 4;
                }
                else if (y <= Console.WindowHeight / 4 - 1)
                {
                    y = 3 * Console.WindowHeight / 4 - 2;
                }
            }
            
            // Метод, управляющий сбором и созданием новой позиции монеты.
            private bool CollectCoin()
            {
                if (X == CoinX && Y == CoinY)
                {
                    _coinCollected = true;
                    
                    if (_type == "*")
                    {
                        AmountCoinPlayer += 1;
                        
                        if (AmountCoinPlayer == 5)
                        {
                            GameOver("Player wins!");
                            return true;
                        }
                    }
                    else
                    {
                        AmountCoinEnemy += 1;
                        
                        if (AmountCoinEnemy == 5)
                        {
                            GameOver();
                            return true;
                        }
                    }
                }
                
                return false;
            }
            
            // Метод, проверяющий, есть ли какой нибудь персонаж или след на данной координате, кроме игрока.
            private static bool IsContains((int, int) coord, int plInd)
            {
                for (int i = 0; i < NpcList.Count; i++)
                {
                    if (coord == (NpcList[i].X, NpcList[i].Y) && i != plInd && 
                        (NpcList[i].X, NpcList[i].Y) != (CoinX, CoinY))
                    {
                        return true;
                    }
                }
                
                return Trace.Contains(coord);
            }
            
            private bool IsContains((int, int) futCoord, (int, int) plCoord)
            {
                for (int i = 0; i < NpcList.Count; i++)
                {
                    if (futCoord == (NpcList[i].X, NpcList[i].Y) && (NpcList[i].X, NpcList[i].Y) != plCoord && 
                        (NpcList[i].X, NpcList[i].Y) != (CoinX, CoinY))
                    {
                        return true;
                    }
                }
                
                return Trace.Contains(futCoord) && _type != "%";
            }
            
            // Метод проверки и отрисовки новой позиции монеты.
            public void Check()
            {
                if (_type != "$")
                {
                    return;
                }
                
                if (_coinCollected)
                {
                    CoinNewPos();
                }
                else
                {
                    Draw();
                }
            }
            
            // Метод отрисовеки новой позиции монеты.
            private void CoinNewPos()
            {
                if (_type == "$")
                {
                    RandomCoor(out CoinX, out CoinY);
                    
                    X = CoinX;
                    Y = CoinY;
                    
                    Draw();
                    
                    _coinCollected = false;
                }
            }
            
            // Метод движение персонажа игры.
            public bool Move(ConsoleKey move)
            {

                if (!IsContains((X, Y), Npc.NpcList.IndexOf(this)))
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                }

                if (_type == "%")
                {
                    Trace.Add((X, Y));
                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("%");
                }

                switch (move)
                {
                    case ConsoleKey.W:
                        Y--;
                        break;
                    case ConsoleKey.A:
                        X--;
                        break;
                    case ConsoleKey.S:
                        Y++;
                        break;
                    case ConsoleKey.D:
                        X++;
                        break;
                    case ConsoleKey.UpArrow:
                        Y--;
                        break;
                    case ConsoleKey.LeftArrow:
                        X--;
                        break;
                    case ConsoleKey.DownArrow:
                        Y++;
                        break;
                    case ConsoleKey.RightArrow:
                        X++;
                        break;
                    case ConsoleKey.Escape:
                        GameOver();
                        return true;
                }
                
                CheckCoord();

                NpcList[NpcList.IndexOf(this)] = this;
                
                Draw();
                
                return CollectCoin();
            }

            private bool Move(ConsoleKey k, int amountMove, (int, int) plCoord)
            {
                bool isEnd = false;
                
                for (int i = 0; i < Trace.Count; i++)
                {
                    Console.SetCursorPosition(Trace[i].Item1, Trace[i].Item2);
                    Console.Write(" ");
                }
                
                Trace.Clear();
                
                for (int i = 0; i < amountMove - 1; i++)
                {
                    isEnd = isEnd || Move(k);
                    
                    if (X == plCoord.Item1 && Y == plCoord.Item2)
                    {
                        isEnd = true;
                    }
                }

                return Move(k) || isEnd;
            }
            public bool Move((int, int) plCoord)
            {
                if (_type == "#" || _type == "%")
                {
                    bool check = true;
                    int move,
                        sum,
                        coorX,
                        coorY,
                        futXp = X + 1,
                        futXm = X - 1,
                        futYp = Y + 1,
                        futYm = Y - 1;

                    List<(int, int)> moves = new List<(int, int)>();
                
                    CheckCoord(ref futXp, ref futYp);
                    CheckCoord(ref futXm, ref futYm);
                
                    moves.Add((X, futYm));
                    moves.Add((futXm, Y));
                    moves.Add((X, futYp));
                    moves.Add((futXp, Y));

                    for (int i = 0; i < 4; i++)
                    {
                        check = check && Trace.Contains(moves[i]);
                    }

                    if (check && _type != "%")
                    {
                        return false;
                    }

                    do
                    {
                        check = false;
                        
                        do
                        {
                            move = Rnd.Next(0, 4);
                        } while (IsContains(moves[move], plCoord));

                        if (_type == "%")
                        {
                            sum = move == 0 ? -1
                                : move == 2 ? 1
                                : move == 1 ? -1
                                : 1;

                            coorX = moves[move].Item1;
                            coorY = moves[move].Item2;
                            
                            for (int i = 0; i < 5; i++)
                            {
                                if (move == 0 || move == 2)
                                {
                                    coorY += sum;
                                }
                                else
                                {
                                    coorX += sum;
                                }

                                CheckCoord(ref coorX, ref coorY);

                                if (IsContains((coorX, coorY), plCoord))
                                {
                                    check = true;
                                    break;
                                }
                            }
                        }
                        
                    } while (check);
                    
                    switch (move)
                    {
                        case 0:
                            return _type == "%" ? Move(ConsoleKey.W, 5, plCoord) : Move(ConsoleKey.W);
                        case 1:
                            return _type == "%" ? Move(ConsoleKey.A, 5, plCoord) : Move(ConsoleKey.A);
                        case 2:
                            return _type == "%" ? Move(ConsoleKey.S, 5, plCoord) : Move(ConsoleKey.S);
                        default:
                            return _type == "%" ? Move(ConsoleKey.D, 5, plCoord) : Move(ConsoleKey.D);
                    }     
                }

                return false;
            }

            private ConsoleKey Move(int plInd,  int min, int disXOut, int disYOut)
            {
                Npc player = NpcList[plInd];
                int disX = Math.Abs(X - player.X), 
                    disY = Math.Abs(Y - player.Y), 
                    futXp = X + 1, 
                    futXm = X - 1, 
                    futYp = Y + 1, 
                    futYm = Y - 1;

                List<(int, int)> moves = new List<(int, int)>();
                
                CheckCoord(ref futXp, ref futYp);
                CheckCoord(ref futXm, ref futYm);
                
                moves.Add((X, futYm));
                moves.Add((futXm, Y));
                moves.Add((X, futYp));
                moves.Add((futXp, Y));
                
                if (min == disY)
                {
                    if (Y > player.Y && !IsContains(moves[0], plInd))
                    {
                        return ConsoleKey.W;
                    }
                    if (Y < player.Y && !IsContains(moves[2], plInd))
                    {
                        return ConsoleKey.S;
                    }
                }
                else if (min == disX)
                {
                    if (X > player.X && !IsContains(moves[1], plInd))
                    {
                        return ConsoleKey.A;
                    }
                    if (X < player.X && !IsContains(moves[3], plInd))
                    {
                        return ConsoleKey.D;
                    }
                }
                else if (min == disXOut)
                {
                    if (X > player.X && !IsContains(moves[3], plInd))
                    {
                        return ConsoleKey.D;
                    }
                    if (X < player.X && !IsContains(moves[1], plInd))
                    {
                        return ConsoleKey.A;
                    }
                }
                else if (min == disYOut)
                {
                    if (Y > player.Y && !IsContains(moves[2], plInd))
                    {
                        return ConsoleKey.S;
                    }
                    if (Y < player.Y && !IsContains(moves[0], plInd))
                    {
                        return ConsoleKey.W;
                    }
                }

                return ConsoleKey.C;
            }
            
            public bool Move(int plInd) 
            {
                Npc player = NpcList[plInd];
                int disX = Math.Abs(X - player.X);
                int disY = Math.Abs(Y - player.Y);
                int disXOut = 2 * Console.WindowWidth / 4 - disX;
                int disYOut = 2 * Console.WindowHeight / 4 - disY;
                
                List<int> disList = new List<int>();
                
                disList.Add(disX);
                disList.Add(disY);
                disList.Add(disXOut);
                disList.Add(disYOut);

                int min = disList.Min();

                if (min == 0)
                {
                    if (min == disY)
                    {
                        disYOut = 0;
                        disList[3] = 0;
                    } 
                    if (min == disX)
                    {
                        disXOut = 0;
                        disList[2] = 0;
                    }
                }

                ConsoleKey move = Move(plInd, min, disXOut, disYOut);
                
                while(move == ConsoleKey.C)
                {
                    do
                    {
                        disList.Remove(min);
                        if (disList.Count > 0)
                        {
                            min = disList.Min();
                        } else
                        {
                            break; 
                        }
                    } while (min == 0);

                    if (disList.Count > 0)
                    {
                        move = Move(plInd, min, disXOut, disYOut);
                    }
                    else
                    {
                        break;
                    }
                    
                }

                return Move(move);
            }
            
        }

        // Метод окончания игры.
        static void GameOver(string message = "Enemies win!")
        {
            Console.Clear();
            DrawMap();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.SetCursorPosition(Console.WindowWidth, Console.WindowHeight);
        }

        // Метод установки настроек консоли.
        static void SetSettings()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }
        
        // Метод, отрисовки поля игры.
        static void DrawMap()
        {
            SetSettings();
             
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4);
            for (int i = 0; i < 2 * Console.WindowHeight / 4; i++)
            {
                Console.Write("|");
                Console.SetCursorPosition(Console.CursorLeft + 2 * Console.WindowWidth / 4, Console.CursorTop);
                Console.Write("|");
                Console.SetCursorPosition(Console.CursorLeft - 2 * Console.WindowWidth / 4 - 1, Console.CursorTop);
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + i + 1);
            }
            
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 - 1);
            Console.Write("_");
            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, Console.WindowHeight / 4 - 1);
            for (int i = 0; i < 2 * Console.WindowWidth / 4 + 2; i++)
            {
                Console.Write("_");
                if (i < 2 * Console.WindowWidth / 4)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 4 + 1 + i, 3 * Console.WindowHeight / 4 - 1);
                    Console.Write("_");
                    Console.SetCursorPosition(Console.WindowWidth / 4 + 1 + i, Console.WindowHeight / 4 - 1);
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Player: {Npc.AmountCoinPlayer}");
            Console.WriteLine($"Enemy: {Npc.AmountCoinEnemy}");
        }
        
        // Метод генерации случайных координат.
        static void RandomCoor(out int rndX, out int rndY)
        {
            bool ready;
            do
            {
                ready = true;
                rndX = Npc.Rnd.Next(Console.WindowWidth / 4 + 1, 3 * Console.WindowWidth / 4);
                rndY = Npc.Rnd.Next(Console.WindowHeight / 4, 3 * Console.WindowHeight / 4 - 1);
                for (int i = 0; i < Npc.NpcList.Count; i++)
                {
                    if (rndX == Npc.NpcList[i].X && rndY == Npc.NpcList[i].Y)
                    {
                        ready = false;
                    }
                }
            } while (!ready || Npc.Trace.Contains((rndX, rndY)));
        }

        // Метод проверки, догнал ли враг игрока.
        static bool CheckGame(int playerInd)
        {
            if (Npc.Trace.Contains((Npc.NpcList[playerInd].X, Npc.NpcList[playerInd].Y)))
            {
                GameOver();
                return true;
            }
            return false;
        }
        
        static bool CheckGame(int playerInd, int enemyInd)
        {
            if ((Npc.NpcList[playerInd].X, Npc.NpcList[playerInd].Y) ==
                (Npc.NpcList[enemyInd].X, Npc.NpcList[enemyInd].Y))
            {
                GameOver();
                return true;
            }

            return false;
        }

        // Метод отрисовки следа, после маштабизации.
        static void DrawTrace()
        {
            int coorX,
                coorY;
            
            for (int i = 0; i < Npc.Trace.Count; i++)
            {
                coorX = Npc.Trace[i].Item1;
                coorY = Npc.Trace[i].Item2;
                coorX = (int)Math.Round(coorX * (Console.WindowWidth / (double)Npc.WindowSetting.Item1), MidpointRounding.ToEven);
                coorY = (int)Math.Round(coorY * (Console.WindowHeight / (double)Npc.WindowSetting.Item2), MidpointRounding.ToEven);

                Npc.CheckCoord(ref coorX, ref coorY);
                
                Npc.Trace[i] = (coorX, coorY);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(coorX, coorY);
                Console.Write("%");
            }
        }
        
        // Метод проверки маштабизации.
        static bool CheckChange()
        {
            if ((Console.WindowWidth, Console.WindowHeight) == Npc.WindowSetting)
            {
                return false;    
            }
            
            Console.Clear();

            DrawMap();

            DrawTrace();
            
            for (int i = 0; i < Npc.NpcList.Count; i++)
            {
                Npc.NpcList[i].ChangeCoord();
            }
            
            Npc.WindowSetting = (Console.WindowWidth, Console.WindowHeight);

            return true;
        }

        // Метод отрисовки меню.
        static ConsoleKey Menu()
        {
            SetSettings();
            Console.SetCursorPosition(0, Console.WindowHeight / 2 - 1);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Добро пожаловать в Невероятные Приключения Звёздочки!");
            Console.WriteLine("Чтобы начать игру, нажмите enter, а чтобы выйти из меню, нажмите q.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("НЕ ЗАБУДЬТЕ ПЕРЕЙТИ НА АНГЛИЙСКУЮ РАСКЛАДКУ!");
            Console.SetCursorPosition(Console.WindowWidth, Console.WindowHeight);
            ConsoleKey key = Console.ReadKey().Key;
            return key;
        }
        
        static void Main()
        {
            // Случайные координаты.
            int rndX, 
                rndY;
            
            // Переменная проверки конца игры.
            bool isEnd;
            
            // Ход игрока.
            ConsoleKey move;

            while (true)
            {
                move = Menu();
                
                if (move == ConsoleKey.Q)
                {
                    SetSettings();
                    return;
                }

                if (move == ConsoleKey.Enter)
                {
                    break;
                }
            }

            DrawMap();
            
            Npc player = new Npc(Console.WindowWidth / 2, Console.WindowHeight / 2, 
                "*", ConsoleColor.Black);
            
            RandomCoor(out rndX, out rndY);
            Npc bizEnemy = new Npc(rndX, rndY, "#", ConsoleColor.White);
            
            RandomCoor(out rndX, out rndY);
            Npc stalEnemy = new Npc(rndX, rndY, "@", ConsoleColor.Red);
            
            RandomCoor(out Npc.CoinX, out Npc.CoinY);
            Npc coin = new Npc(Npc.CoinX, Npc.CoinY, "$", ConsoleColor.Yellow);
            
            RandomCoor(out rndX, out rndY);
            Npc charEnemy = new Npc(rndX, rndY, "%", ConsoleColor.Magenta);

            while (true)
            {

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                move = Console.ReadKey().Key;
                
                if (CheckChange())
                {
                    player = Npc.NpcList[Npc.NpcList.IndexOf(player)];
                    bizEnemy = Npc.NpcList[Npc.NpcList.IndexOf(bizEnemy)];
                    stalEnemy = Npc.NpcList[Npc.NpcList.IndexOf(stalEnemy)];
                    coin = Npc.NpcList[Npc.NpcList.IndexOf(coin)];
                    charEnemy = Npc.NpcList[Npc.NpcList.IndexOf(charEnemy)];
                }
                
                isEnd = player.Move(move);
                
                if (isEnd || CheckGame(Npc.NpcList.IndexOf(player)))
                {
                    break;
                }
                
                coin.Check();
                
                isEnd = bizEnemy.Move((player.X, player.Y));
                
                if (isEnd || CheckGame(Npc.NpcList.IndexOf(player), Npc.NpcList.IndexOf(bizEnemy)))
                {
                    break;
                }
                
                coin.Check();
                
                isEnd = stalEnemy.Move(Npc.NpcList.IndexOf(player));
                
                if (isEnd || CheckGame(Npc.NpcList.IndexOf(player), Npc.NpcList.IndexOf(stalEnemy)))
                {
                    break;
                }
                
                coin.Check();

                isEnd = charEnemy.Move((player.X, player.Y));
                
                if (isEnd)
                {
                    GameOver();
                    break;
                }
                
                if (CheckGame(Npc.NpcList.IndexOf(player), Npc.NpcList.IndexOf(charEnemy)))
                {
                    break;
                }
                
                coin.Check();
            }
        }
    }
}