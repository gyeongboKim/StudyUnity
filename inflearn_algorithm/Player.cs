using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inflearn_algorithm
{
    class Pos 
    { 
        public Pos(int y, int x) { Y = y; X = x;}
        public int Y;
        public int X;
    }
    internal class Player
    {
        //외부에서 플레이어의 위치를 질의할 수는 있겠지만 강제로 수정할 수 없게 막아버림
        //책임론
        //플레이어만 좌표를 고칠 수 있도록. 단, 초기 좌표만 예외적으로 public

        public int PosY { get; private set; }
        public int PosX { get; private set; }
        
        Random _random = new Random();
        Board _board;

        enum Direction 
        { 
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }
        int _dir = (int)Direction.Up;
        List<Pos> _paths = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)     //Program 에서 player.Initialize(1,1,board);
        {
            PosX = posX;
            PosY = posY;
            _board = board;

            AStar();
            //BFS();
        }

        struct PQNode : IComparable<PQNode>
        {
            public int F;
            public int G;
            public int Y;
            public int X;

            public int CompareTo(PQNode other)
            {
                if (F == other.F)
                    return 0;
                return F < other.F ? 1 : -1;
            }
        }
        void AStar()
        {

            int[] deltaY = new int[] { -1, 0, 1, 0, -1, 1, 1, -1 };//Up, Left, Down, Right    +UL DL DR UR
            int[] deltaX = new int[] { 0, -1, 0, 1, -1, -1, 1, 1 };//Up, Left, Down, Right
            int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14 };
            //점수 매기기
            //F = G + H
            //F = 최종 점수(작을 수록 좋음, 경로에 따라 달라짐)
            //G = 시작점에서 해당 좌표 까지 이동하는데 드는 비용( 작을 수록 좋음, 경로에 따라 달라짐)
            //H = 목적지에서 얼마나 가까운지 (작을 수록 좋음, 고정)

            //(y,x) 이미 방문했는지 여부 (방문 = closed)
            bool[,] closed = new bool[_board.Size, _board.Size]; //CloseList

            //(y, x) 가는 길을 한 번이라도 발견했는지
            //발견 X => MaxValue
            //발견 O -> F = G + H
            int[,] open = new int[_board.Size, _board.Size];
            for (int y = 0; y < _board.Size; y++)
                for (int x = 0; x < _board.Size; x++)
                    open[y, x] = Int32.MaxValue;

            Pos[,] parent = new Pos[_board.Size, _board.Size];

            //open 리스트에 있는 정보들 중에서, 가장 좋은 후보를 빠르게 뽑아오기 위한 도구
            PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

            //시작점 발견 (예약 진행)
            open[PosX, PosY] = 10*(Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX));       //절대값 반환
            pq.Push(new PQNode() { F = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX)), G = 0, Y = PosY, X = PosX });
            parent[PosY, PosX] = new Pos(PosY, PosX);

            bool foundPath = false;
            while(pq.Count > 0)
            {
                //제일 좋은 후보를 찾는다.
                PQNode node = pq.Pop();
                //동일한 좌표를 여러 경로로 찾아서, 더 빠른 경로로 인해서 이미 방문(closed)된 경우 스킵
                if (closed[node.Y, node.X])
                    continue;

                // 방문한다
                closed[node.Y, node.X] = true;
                //목적지에 도착한 경우 바로 종료
                if (node.Y == _board.DestY && node.X == _board.DestX)
                {
                    foundPath = true;
                    break;
                }

                // 이동할 수 있는 좌표인지 확인해서 예약(open)한다.
                for (int i = 0; i < deltaY.Length; i++)
                {
                    int nextY = node.Y + deltaY[i];
                    int nextX = node.X + deltaX[i];

                    // 유효범위를 벗어난 경우 스킵
                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
                    // 연결되어 있지 않은 경우(벽인 경우)
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    //이미 발견한 경우 continue
                    if (closed[nextY, nextX])
                        continue;

                    //비용 계산
                    int g = node.G + cost[i];
                    int h =10 * (Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX));
                    //다른 경로에서 더 빠른 길을 이미 찾은 경우 스킵
                    if (open[nextY, nextX] < g+h)
                        continue;

                    //예약 진행
                    open[nextY, nextX] = g + h;
                    pq.Push(new PQNode() { F = g + h, G = g, Y = nextY, X = nextX });
                    parent[nextY, nextX] = new Pos(node.Y, node.X);
                }
            }

            if(foundPath)
                CalcPathFromParent(parent);
        }
        void BFS()
        {
            int[] deltaY = new int[] { -1, 0, 1, 0 };//Up, Left, Down, Right
            int[] deltaX = new int[] { 0, -1, 0, 1};//Up, Left, Down, Right

            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            Queue<Pos> queue = new Queue<Pos>();
            queue.Enqueue(new Pos(PosY, PosX));
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while (queue.Count > 0)
            {
                Pos pos = queue.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;

                for(int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];
                    //유효범위를 벗어난 경우 스킵
                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
                    //연결되어 있지 않은 경우(벽인 경우)
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    //이미 발견한 경우 continue
                    if (found[nextY, nextX])
                        continue;

                    queue.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
                //인접하지 않은 경우 continue
                //큐에서 꺼낸 노드의 인접한 노드 enqueue
            }

            CalcPathFromParent(parent);
        }

        void CalcPathFromParent(Pos[,] parent)
        {
            int y = _board.DestY;
            int x = _board.DestX;
            //parent의 좌표와 내 자신의 좌표가 같을 때 까지(시작점인 루트노드 까지) 실행
            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _paths.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            //시작점을 추가해줌
            _paths.Add(new Pos(y, x));
            //리스트의 데이터들을 역으로 바꿔주는 함수
            _paths.Reverse();
        }


        void RightHand()
        {
            //현재 바라보는 방향을 기준으로 앞 칸으로의 좌표 변화
            int[] frontY = new int[] { -1, 0, 1, 0 }; //Up Left Down Right
            int[] frontX = new int[] { 0, -1, 0, 1 }; //Up Left Down Right

            //현재 바라보는 방향을 기준으로 오른쪽 칸으로의 좌표 변화
            int[] rightY = new int[] { 0, -1, 0, 1 };   //Up Left Down Right
            int[] rightX = new int[] { 1, 0, -1, 0 };   //Up Left Down Right

            _paths.Add(new Pos(PosY, PosX));
            // 목적지에 도착하기 전에는 계속 실행
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                //1. 현재 바라보는 방향을 기준으로 오른쪽칸으로 갈 수 있는지 확인
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;

                    //앞으로 한 보 전진(현재 방향을 기준으로)
                    //배열을 통해 한번에 처리
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _paths.Add(new Pos(PosY, PosX));

                }
                //2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 화인
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    //앞으로 한 칸 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _paths.Add(new Pos(PosY, PosX));
                }
                else
                {
                    //왼쪽 방향으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        const int MOVE_TICK = 50;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_lastIndex >= _paths.Count)
            {
                _lastIndex = 0;
                _paths.Clear();
                _board.Initialize(_board.Size, this);
                Initialize(1, 1, _board);
            }

                
            if(_sumTick >= MOVE_TICK )
            {
                _sumTick = 0;

                PosY = _paths[_lastIndex].Y;
                PosX = _paths[_lastIndex].X;
                _lastIndex++;
                //int randValue = _random.Next(0, 5);
                //switch (randValue) 
                //{
                //    case 0:         //상 (랜덤 방향이 위일 때, Empty 타일인 경우)
                //        if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                //            PosY = PosY - 1;
                //        break;
                //    case 1:         //하 (랜덤 방향이 아래일 때, Empty타일인 경우)
                //        if(PosX + 1 < _board.Size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                //            PosY = PosY + 1;
                //        break;
                //    case 2:         //좌
                //        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                //            PosX = PosX - 1;
                //        break;
                //    case 3:         //우
                //        if (PosX + 1 < _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                //            PosX = PosX + 1; ;
                //        break;
                //    default:
                //        break;
                //}

                //0.1초마다 실행될 로직을 넣어줌
            }

        }
    }
}
