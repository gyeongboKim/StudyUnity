using System;

namespace inflearn_algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            
            int lastTick = 0;
            while (true)
            {
                #region 프레임관리
                int currentTick = System.Environment.TickCount;  //시스템이 시작된 이후에 경과된 ms를 반환
                //만약에 경과한 시간이 1/30초보다 작다면
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;

                #endregion
                //입력

                //로직
                player.Update(deltaTick);

                //렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();

            }
        }
    }
}

