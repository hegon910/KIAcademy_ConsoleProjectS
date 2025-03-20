using System.ComponentModel.Design;

namespace _250319_플밍_수업
{
    internal class Program
    {
        //게임 목표, 장애물을 치워서 목표 지점에 도달하는 게임.
        //설계
        //캐릭터, 장애물, 목표가 필요
        struct Position
        {
            public int x;
            public int y;
        }
        //장애물은 이동가능해야함

        //목표 지점에 도착하면 클리어
        //함정(방해) 요소 설치


        static void Main(string[] args)
        {
                bool gameOver = false;
            Position playerPos;
            char[,] map;
            //게임 시작
            Console.CursorVisible = false;
            Console.WriteLine("게임을 시작합니다. 장애물을 치우고, 목표 지점까지 도달하세요!");
            //준비
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press Enter to START!");
            Console.ResetColor();
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            { }
                Console.Clear();
                Start(out playerPos, out map);


                while (!gameOver)
                //진행
                {

                    Render(playerPos, map);
                    ConsoleKey key = Input();



                    Update(key, ref playerPos, map);



                if (IsClear(ref playerPos, map, ref gameOver))
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.R)
                    {
                        Console.Clear();
                        Start(out playerPos, out map);
                        gameOver = false;
                    }


                }

        
            }







        }

        static void Start(out Position playerPos, out char[,] map)
        {

            Console.CursorVisible = false;

            playerPos.x = 1;
            playerPos.y = 1;

            string whereami;
            

            map = new char[10, 10]
{
                 { 'X','X','X','X','X','X','X','X','X','X'},
                 { 'X',' ','X',' ',' ',' ',' ','X','G','X'},
                 { 'X',' ','X','O',' ','X',' ','X',' ','X'},
                 { 'X',' ','X',' ',' ',' ','O','X',' ','X'},
                 { 'X',' ','X',' ','O','O',' ','X',' ','X'},
                 { 'X',' ','X',' ',' ','X',' ',' ',' ','X'},
                 { 'X',' ','X','O',' ','X',' ','X',' ','X'},
                 { 'X',' ','X',' ','O',' ','O','X','X','X'},
                 { 'X',' ',' ',' ',' ','X',' ','X','X','X'},
                 { 'X','X','X','X','X','X','X','X','X','X'},
};

        }

        static ConsoleKey Input()
        {

            return Console.ReadKey(true).Key;

        }
        static void Render(Position playerPos, char[,] map)
        {
            
            Console.SetCursorPosition(0, 0);            
            Printmap(map);
            PrintPlayer(playerPos, map);
           

        }
        
        static void Printmap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Where is the 'G'oal? \nPush the 'O' to make a Way!");
        }
        static void PrintPlayer(Position playerPos, char[,] map)
        {
            
            Console.SetCursorPosition(playerPos.x, playerPos.y);

            if (map[playerPos.y, playerPos.x] == 'G')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("G");
            }
            else
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("P");
                }
                Console.ResetColor();
            }

        static void Update(ConsoleKey key, ref Position PlayerPos, char[,] map)
        {
           
            MoveObject(ref PlayerPos, key, map);
            
        }
        static void MoveObject(ref Position playerPos, ConsoleKey Key, char[,] map)
        {
            Position protagoPos;
            Position objectPos;

            switch (Key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    protagoPos.x = playerPos.x - 1;
                    protagoPos.y = playerPos.y;
                    objectPos.x = playerPos.x - 2;
                    objectPos.y = playerPos.y;
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    protagoPos.x = playerPos.x + 1;
                    protagoPos.y = playerPos.y;
                    objectPos.x = playerPos.x + 2;
                    objectPos.y = playerPos.y;
                    break;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    protagoPos.x = playerPos.x;
                    protagoPos.y = playerPos.y - 1;
                    objectPos.x = playerPos.x;
                    objectPos.y = playerPos.y - 2;
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    protagoPos.x = playerPos.x;
                    protagoPos.y = playerPos.y + 1;
                    objectPos.x = playerPos.x;
                    objectPos.y = playerPos.y + 2;
                    break;

                default:
                    return;

                   


            }

            if (map[protagoPos.y, protagoPos.x] == ' ')
            {
                playerPos.x = protagoPos.x;
                playerPos.y = protagoPos.y;
            }

            
            //장애물을 치우자
            if (map[protagoPos.y, protagoPos.x] == 'O')
            {    //장애물 앞이 빈칸
                if (map[objectPos.y, objectPos.x] == ' ')
                {   //빈칸박스
                    map[objectPos.y, objectPos.x] = 'O';
                }
                //박스빈칸
                
                //플레이어 이동
                

                //장애물 앞이 장애물
                else
                {
                    return;
                }
                
                map[protagoPos.y, protagoPos.x] = ' ';
                playerPos.x = protagoPos.x;
                playerPos.y = protagoPos.y;

            }
                //G면 덮어
            if (map[protagoPos.y, protagoPos.x] == 'G')
            {

                playerPos.x = protagoPos.x;
                playerPos.y = protagoPos.y;
                return;
            }

            if (map[protagoPos.y, protagoPos.x] == 'X')
            {
                return;
            }

            //배열 나가지 말게 하기...
            if (protagoPos.x < 0 || protagoPos.x >= map.GetLength(1) || protagoPos.y < 0 || protagoPos.y >= map.GetLength(0)) 
            {
                return;
            }

        }
        static bool IsClear(ref Position playerPos, char[,] map, ref bool gameOver)
        {
            PrintPlayer(playerPos, map);
            if (map[playerPos.y, playerPos.x] == 'G')
            {
                
                Console.SetCursorPosition(0, map.GetLength(0) + 2);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Clear!Q(^_^ Q)");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Press 'R' to Restart");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("다른 버튼을 누르면 게임을 종료합니다.");
                Console.ResetColor();
                gameOver = true;

                return true;

            }


            return false;

        }
   


    }
}
